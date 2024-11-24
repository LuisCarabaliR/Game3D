using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText; // Texto que muestra la puntuación
    [SerializeField] private GameObject gameOverPanel; // Panel de "Game Over"
    [SerializeField] private GameObject pausePanel; // Panel de pausa
    [SerializeField] private Button pauseButton; // Botón de pausa en la UI
    [SerializeField] private Button resumeButton; // Botón de reanudar en la UI

    private int score; // Puntuación del jugador
    private bool isGamePaused = false; // Indica si el juego está en pausa

    private void Start()
    {
        // Inicializar puntuación y desactivar paneles
        score = 0;
        UpdateScore();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);

        // Asegurar que el juego comience sin pausas
        Time.timeScale = 1f;

        // Asegurarnos de que los botones estén vinculados a sus funciones
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(PauseGame);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(ResumeGame);
        }
    }

    private void Update()
    {
        // Detectar la tecla de escape para pausar/reanudar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Incrementar el puntaje si el juego no está pausado ni ha terminado
        if (!isGamePaused && (gameOverPanel == null || !gameOverPanel.activeSelf))
        {
            IncrementScore();
        }
    }

    private void IncrementScore()
    {
        score += 1; // Incrementar el puntaje
        UpdateScore();
    }

    public void EndGame()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0; // Detener el juego
        }
        else
        {
            Debug.LogError("gameOverPanel no está asignado en el Inspector.");
        }
    }

    public void RestartGame()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        Time.timeScale = 1; // Reanudar el juego
        score = 0;
        UpdateScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar escena
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isGamePaused);
        }

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(!isGamePaused); // El botón de pausa se oculta cuando está pausado
        }

        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    public void PauseGame() // Método llamado por el botón de pausa
    {
        isGamePaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null) pausePanel.SetActive(true);
        if (pauseButton != null) pauseButton.gameObject.SetActive(false); // El botón de pausa desaparece
    }

    public void ResumeGame() // Método llamado por el botón de reanudar
    {
        isGamePaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null) pausePanel.SetActive(false);
        if (pauseButton != null) pauseButton.gameObject.SetActive(true); // El botón de pausa vuelve a aparecer
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("scoreText no está asignado en el Inspector o no se encontró en la escena.");
        }
    }
}
