using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using System.Collections; // Required for Coroutines

using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnChance = 0.2f; // 20% chance to spawn power-up
    public float spawnSpeed = 3f; // Speed at which the power-up moves upwards

    public void SpawnPowerUp(Vector3 spawnPosition)
    {
        if (Random.value <= spawnChance)
        {
            GameObject powerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = powerUp.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.up * spawnSpeed;
            }
            else
            {
                Debug.LogError("Power-up prefab does not have a Rigidbody2D!");
            }
        }
    }
}