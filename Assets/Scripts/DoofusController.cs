using UnityEngine;

public class DoofusController : MonoBehaviour
{
    private float speed;
    private bool gameStarted = false;
    private GameOverManager gameOverManager;
    private JSONReader jsonReader;

    void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();
        jsonReader = FindObjectOfType<JSONReader>();
        speed = jsonReader.gameParameters.player_data.speed;
    }

    void Update()
    {
        if (!gameStarted) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (transform.position.y < -1)
        {
            gameOverManager.GameOver();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}