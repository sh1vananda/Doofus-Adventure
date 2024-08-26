using UnityEngine;
using TMPro; // Assuming you're using TextMeshPro for UI text

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the UI Text element
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
        scoreText.text = "Score: " + score.ToString(); // Update the text element
    }
}