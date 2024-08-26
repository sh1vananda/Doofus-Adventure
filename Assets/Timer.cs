using UnityEngine;
using TMPro;

public class PulpitTimer : MonoBehaviour
{
    private float countdownTime;
    private float currentTime;
    private TMP_Text timerText;

    void Start()
    {
        timerText = GetComponentInChildren<TMP_Text>(); // Find the TimerText component
        if (timerText == null)
        {
            Debug.LogError("TimerText component not found on Pulpit.");
        }
        ResetTimer(); // Initialize the timer
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            // Ensure the timer text shows zero when time is up
            currentTime = 0;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int seconds = Mathf.FloorToInt(currentTime);
            int milliseconds = Mathf.FloorToInt((currentTime - seconds) * 100);
            timerText.text = string.Format("{0}.{1:00}", seconds, milliseconds); // Display seconds and milliseconds
        }
    }

    public void SetCountdownTime(float time)
    {
        countdownTime = time;
        ResetTimer(); // Reset the timer with the new countdown time
    }

    private void ResetTimer()
    {
        currentTime = countdownTime; // Initialize current time with the countdown time
    }
}