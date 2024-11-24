using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Speed, Jump }
    public PowerUpType powerUpType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerUpType == PowerUpType.Speed)
            {
                other.GetComponent<PlayerController>().moveSpeed *= 2; // Doble velocidad
            }
            else if (powerUpType == PowerUpType.Jump)
            {
                other.GetComponent<PlayerController>().jumpForce *= 2; // Doble salto
            }
            Destroy(gameObject); // Destruir powerup
        }
    }
}
