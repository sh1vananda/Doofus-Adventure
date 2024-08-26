using UnityEngine;

public class PulpitTriggerHandler : MonoBehaviour
{
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Find the ScoreManager in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Doofus")) // Ensure Doofus is tagged correctly
        {
            Debug.Log("Doofus entered the Pulpit trigger zone.");
            scoreManager.IncrementScore(); // Increment the score
        }
    }
}