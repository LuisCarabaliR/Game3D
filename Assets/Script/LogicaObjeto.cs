using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaObjeto : MonoBehaviour
{
    public bool destruirConCursor;
    public bool destruirAutomatico;
    public LogicaPersonaje logicaPersonaje;
    public int tipo;

    // 1 = Crece
    // 2 = Aumenta Velocidad
    // 3 = Aumenta Salto
    // 4 = Disminuye Velocidad

    // Variables públicas serializadas
    /*[SerializeField] private GameObject efecto;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    // Método que se llama al inicio
    void Start()
    {
        logicaPersonaje = GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaPersonaje>();
    }
*/
    // Método que se llama cuando otro objeto entra en el collider
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar puntos
            puntaje.SumarPuntos(cantidadPuntos);

            // Aplicar el efecto correspondiente
            Efecto();

            // Verificar si el prefab de efecto está asignado
            if (efecto != null)
            {
                // Instanciar el efecto en la posición del objeto
                Instantiate(efecto, transform.position, Quaternion.identity);
            }
            else
            {
                // Si no está asignado, mostrar un mensaje de advertencia en la consola
                Debug.LogWarning("El efecto no está asignado en el script Destruir.");
            }

            // Destruir el objeto que tiene este script
            Destroy(gameObject);
        }
    }*/

    // Método para aplicar los efectos
    public void Efecto()
    {
        switch (tipo)
        {
            case 1:
                logicaPersonaje.gameObject.transform.localScale = new Vector3(3, 3, 3);  // Hacer que crezca
                break;
            case 2:
                logicaPersonaje.velocidadMovimiento += 5;  // Aumentar la velocidad
                break;
            case 3:
                logicaPersonaje.fuerzaDeSalto += 2;  // Aumentar el salto
                break;
            case 4:
                logicaPersonaje.velocidadMovimiento -= 3;   // Disminuye la velocidad
                break;

            default:
                Debug.Log("Sin efecto asignado");
                break;
        }
    }

    // Método Update que maneja la destrucción de objetos con el cursor
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // Cuando se hace clic derecho
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject hitObject = hitInfo.collider.gameObject;
                LogicaObjeto logicaObjeto = hitObject.GetComponent<LogicaObjeto>();

                if (logicaObjeto != null && logicaObjeto.destruirConCursor == true)
                {
                    logicaObjeto.Efecto();
                    Destroy(hitObject);
                }
            }
        }
    }

    // Método único OnTriggerStay para manejar la lógica de objetos automáticamente y con clic derecho
    private void OnTriggerStay(Collider other)
    {
        LogicaObjeto logicaObjeto = other.GetComponent<LogicaObjeto>();

        if (logicaObjeto != null)
        {
            // Si el objeto debe destruirse automáticamente
            if (logicaObjeto.destruirAutomatico)
            {
                logicaObjeto.Efecto();
                Destroy(other.gameObject);
            }
            // Si el objeto puede ser destruido con clic derecho
            else if (Input.GetMouseButtonDown(1) && !logicaObjeto.destruirConCursor)
            {
                logicaObjeto.Efecto();
                Destroy(other.gameObject);
            }
        }
    }
}
