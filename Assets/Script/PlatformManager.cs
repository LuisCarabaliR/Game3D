using UnityEngine;

public class PlatformLooper : MonoBehaviour
{
    public GameObject platformPrefab;  // Prefab de la plataforma
    public Transform player;          // Referencia al jugador
    public float platformLength = 10f; // Longitud de la plataforma en el eje Z
    public float jumpGap = 2f;        // Espacio entre plataformas para saltar
    public float activationDistance = 1f; // Distancia para activar el movimiento de la plataforma

    private GameObject platform1; // Primera plataforma
    private GameObject platform2; // Segunda plataforma
    private GameObject nextPlatformToMove; // Plataforma que será movida

    void Start()
    {
        // Crear las dos plataformas iniciales alineadas con el jugador
        Vector3 initialPosition = new Vector3(player.position.x, 0, player.position.z);
        platform1 = Instantiate(platformPrefab, initialPosition, Quaternion.identity);
        platform2 = Instantiate(platformPrefab, initialPosition + Vector3.forward * (platformLength + jumpGap), Quaternion.identity);

        nextPlatformToMove = platform1; // Inicialmente, la plataforma 1 será la próxima en moverse
    }

    void Update()
    {
        // Verificar si el jugador está cerca del borde de una plataforma
        if (IsPlayerNearEdge(platform1) && nextPlatformToMove == platform1)
        {
            MovePlatform(platform1, platform2.transform.position.z + platformLength + jumpGap);
            nextPlatformToMove = platform2;
        }
        else if (IsPlayerNearEdge(platform2) && nextPlatformToMove == platform2)
        {
            MovePlatform(platform2, platform1.transform.position.z + platformLength + jumpGap);
            nextPlatformToMove = platform1;
        }
    }

    bool IsPlayerNearEdge(GameObject platform)
    {
        // Calcular la posición del borde de la plataforma
        float platformEndZ = platform.transform.position.z + platformLength / 2;
        return player.position.z >= platformEndZ - activationDistance;
    }

    void MovePlatform(GameObject platform, float newZPosition)
    {
        // Asegurar que la plataforma se mueva correctamente frente al jugador
        platform.transform.position = new Vector3(player.position.x, 0, newZPosition);
    }
}
