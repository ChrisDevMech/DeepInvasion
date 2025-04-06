using UnityEngine;
using TMPro; // Required for TextMesh Pro

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerUpTimerText;

    public PlayerController player; // Assign your player GameObject in the Inspector

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject not assigned to UIController!");
        }

        if (livesText == null || powerUpTimerText == null)
        {
            Debug.LogError("TextMeshProUGUI components not assigned to UIController!");
        }
    }

    void Update()
    {
        if (player != null && livesText != null)
        {
            UpdateLivesUI();
        }

        if (player != null && powerUpTimerText != null)
        {
            UpdatePowerUpTimerUI();
        }
    }

    void UpdateLivesUI()
    {
        livesText.text = "Vidas: " + player.lives;
    }

    void UpdatePowerUpTimerUI()
    {
        if (player.isPoweredUp)
        {
            powerUpTimerText.text = "Power-Up: " + Mathf.Ceil(player.powerUpTimer).ToString(); // Display remaining time
        }
        else
        {
            powerUpTimerText.text = ""; // Hide the timer when the power-up is inactive
        }
    }
}