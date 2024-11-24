using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour
{
    public LogicaPersonaje logicaPersonaje;
    public int tipo;

    // 1 = Crece
    // 2 = Velocidad Aumentada
    // 3 = Salto Aumentado
    // 4 = Muere
    // 5 = Recoge Arma
    // Variables públicas serializadas
    
    [SerializeField] private GameObject efecto;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    // Método que se llama cuando otro objeto entra en el collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar puntos
            puntaje.SumarPuntos(cantidadPuntos);
            
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
    }
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

    // Método Update vacío (si no lo necesitas, puedes eliminarlo)
    void Update()
    {
        
    }
}
