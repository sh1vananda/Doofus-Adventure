using UnityEngine;
using UnityEngine.UI;

public class StartScreenManager : MonoBehaviour
{
    public Button startGameButton;
    public GameObject doofusController;

    void Start()
    {
        // Ensure the game is paused initially
        Time.timeScale = 0;
        startGameButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        // Unpause the game and hide the start screen
        Time.timeScale = 1;
        gameObject.SetActive(false);

        // Enable player controls
        doofusController.GetComponent<DoofusController>().StartGame();
    }
}