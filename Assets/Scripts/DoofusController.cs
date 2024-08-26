using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 20.0f; // Speed of Doofus's movement
    private ScoreManager scoreManager; // Reference to the ScoreManager

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager in the scene
    }

    void Update()
    {
        // Handle movement input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 for movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the character
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if Doofus has entered a Pulpit
        if (other.CompareTag("Pulpit"))
        {
            scoreManager.IncrementScore(); // Increment the score
            // Optional: Additional logic for when Doofus moves onto a new Pulpit
        }
    }
}