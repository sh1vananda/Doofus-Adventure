using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Assign the Game Over panel in the Inspector
    public Button restartButton; // Assign the Restart button in the Inspector

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide the Game Over screen initially
        restartButton.onClick.AddListener(RestartGame);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true); // Show the Game Over screen
        Time.timeScale = 0; // Pause the game
    }

    private void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}