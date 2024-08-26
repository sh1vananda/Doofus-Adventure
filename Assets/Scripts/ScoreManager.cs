using UnityEngine;
using TMPro; // Assuming you're using TextMeshPro for UI text

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the UI Text element
    private int score = 0; // Initial score

    void Start()
    {
        // Ensure scoreText is assigned
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector!");
            return;
        }
        UpdateScoreText(); // Initialize the score display
    }

    public void IncrementScore()
    {
        score++; // Increment the score
        UpdateScoreText(); // Update the UI
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); // Update the text element
        }
        else
        {
            Debug.LogWarning("ScoreText reference is None, cannot update.");
        }
    }
}