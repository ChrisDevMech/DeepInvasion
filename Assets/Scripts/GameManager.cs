using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using System.Collections; // Required for Coroutines

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public SplinePath[] enemyPaths; // Array of SplinePath scripts
    public float enemySpawnRate = 2f; // Enemies per second
    public PlayerController player; // Reference to the player's script
    public Text livesText; // UI Text to display player lives
    public GameObject powerupPrefab; // Powerup prefab
    public float powerupSpawnInterval = 10f; // Time between powerup spawns
    public float powerupDuration = 5f; // Duration of the powerup effect

    private bool powerupActive = false;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not assigned!");
        }

        if (livesText == null)
        {
            Debug.LogError("Lives Text UI element not assigned!");
        }

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
    }

    void Update()
    {
        if (player != null)
        {
            livesText.text = "Lives: " + player.lives;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemyPaths.Length > 0 && enemyPrefab != null)
            {
                SplinePath randomPath = enemyPaths[Random.Range(0, enemyPaths.Length)];
                Instantiate(enemyPrefab, randomPath.GetPoint(0), Quaternion.identity); // Spawn at start of path
            }

            yield return new WaitForSeconds(1f / enemySpawnRate);
        }
    }

    IEnumerator SpawnPowerups()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerupSpawnInterval);

            if (powerupPrefab != null)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)); // Adjust spawn area
                Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    public void ActivatePowerup()
    {
        if (!powerupActive)
        {
            powerupActive = true;
            player.moveSpeed *= 2f; // Example: Double player speed

            StartCoroutine(DeactivatePowerup());
        }
    }

    IEnumerator DeactivatePowerup()
    {
        yield return new WaitForSeconds(powerupDuration);

        player.moveSpeed /= 2f; // Restore original speed
        powerupActive = false;
    }
}