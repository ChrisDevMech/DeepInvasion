using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float powerUpDuration = 5f; // Duration of power-up effect

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Power");
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivatePowerUp(); // Activate the player's power-up
                Destroy(gameObject); // Destroy the power-up item
            }
        }
    }
}