using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPersonaje : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    public float velocidadCorrer = 10.0f; // Velocidad al correr
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaDeSalto = 8f;
    public bool puedoSaltar; // Esta variable se actualizará para controlar si el personaje puede saltar

    public float velocidadInicial;
    public float velocidadAgachado;
    
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();

        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.5f;
    }

    void FixedUpdate()
    {
        // Movimiento y rotación
        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
    }

    void Update()
    {
        // Entrada del teclado para movimiento
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Detectar si el jugador está corriendo
        if (Input.GetKey(KeyCode.LeftShift) && y > 0) // Solo correr hacia adelante
        {
            velocidadMovimiento = velocidadCorrer;
            anim.SetBool("Corriendo", true);
        }
        else
        {
            velocidadMovimiento = 5.0f; // Velocidad normal
            anim.SetBool("Corriendo", false);
        }

        // Actualizar animaciones
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        // Salto
        if (puedoSaltar) // Solo permitir saltar si está en el suelo
        {
            
            if (Input.GetKeyDown(KeyCode.Space)) // Saltar con la tecla "Space"
            {
                anim.SetBool("Salte", true);
                rb.AddForce(new Vector3(0, fuerzaDeSalto, 0), ForceMode.Impulse);
            }

             if (Input.GetKey(KeyCode.LeftControl)) 
            {
                anim.SetBool("Agachado", true);
                velocidadMovimiento = velocidadAgachado;
            }
            else
            {
                anim.SetBool("Agachado", false);
                velocidadMovimiento = velocidadInicial;
            }
            anim.SetBool("TocoSuelo", true);    
        }
           else
        {
            EstoyCayendo();
        }
    }


    public void EstoyCayendo()
    {
        anim.SetBool("TocoSuelo", false);
        anim.SetBool("Salte", false);
    }
}
