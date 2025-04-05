using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading the scene

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;
    public int lives = 3;
    public bool isPoweredUp = false;
    public float powerUpTimer = 0f;
    public float powerUpDuration = 5f; // Duration of the power-up in seconds

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player!");
        }

        if (projectileSpawnPoint == null)
        {
            Debug.LogError("Projectile Spawn Point not assigned!");
        }
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        rb.linearVelocity = movement * moveSpeed;

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            // GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            GameObject projectile = ObjectPoolControler.instance.GetPooledPlayer();
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectile.transform.position = projectileSpawnPoint.position;               
                projectile.SetActive(true);
                projectileRb.linearVelocity = Vector2.down * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D!");
            }
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add death effects, animations, etc.
        // Reload the current scene (or go to a game over screen)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }

    public void ActivatePowerUp()
    {
        isPoweredUp = true;
        powerUpTimer = powerUpDuration;
        StartCoroutine(PowerUpCountdown());
    }

    IEnumerator PowerUpCountdown()
    {
        while (powerUpTimer > 0)
        {
            yield return null;
            powerUpTimer -= Time.deltaTime;
        }

        isPoweredUp = false;
        powerUpTimer = 0;
    }

    public void TakeDamage(int damageAmount = 1)
    {
        lives -= damageAmount;

        if (lives <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Player took damage. Lives remaining: " + lives);
        }
    }
}