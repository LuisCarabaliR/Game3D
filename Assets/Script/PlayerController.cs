using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 10f; // Fuerza de salto
    public float gravity = 9.8f; // Fuerza de gravedad, puedes eliminar esto si usas el Rigidbody para controlarla

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;
    private float currentVelocityY;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody
    }

    void Update()
    {
        // Comprobar si el jugador está tocando el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);

        if (isGrounded)
        {
            // Movimiento lateral (izquierda/derecha)
            float horizontalInput = Input.GetAxis("Horizontal"); // Movimiento horizontal
            float verticalInput = Input.GetAxis("Vertical"); // Movimiento hacia adelante/atrás (eje Z)

            moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized * moveSpeed;
        }

        // Salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Se aplica un impulso hacia arriba
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Aplicar movimiento horizontal y vertical
        Vector3 targetVelocity = moveDirection;
        targetVelocity.y = rb.velocity.y; // Mantener la velocidad en Y para que no se altere con el movimiento horizontal

        // Establecer la nueva velocidad
        rb.velocity = targetVelocity;
    }

    void FixedUpdate()
    {
        // Si deseas control manual de la gravedad, aquí puedes ajustar la gravedad, pero no es necesario si usas el Rigidbody
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }
}
