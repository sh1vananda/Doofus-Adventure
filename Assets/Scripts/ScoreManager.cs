using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Use TMP_Text for TextMeshPro
    private int score = 0; // Initial score

    void Start()
    {
        UpdateScoreText(); // Initialize the score display
    }

    public void IncrementScore()
    {
        score++; // Increment the score
        UpdateScoreText(); // Update the UI
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}"; // Update the text element
    }
}