using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 5f;
    private bool gameStarted = false;
    private GameOverManager gameOverManager;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    void Update()
    {
        if (!gameStarted) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Check if Doofus falls off the Pulpit
        if (transform.position.y < -1) // Adjust the threshold as needed
        {
            gameOverManager.GameOver();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}