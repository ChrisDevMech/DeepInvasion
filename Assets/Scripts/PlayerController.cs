using UnityEngine;
using UnityEngine.SceneManagement; // Required for reloading the scene

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;
    public int lives = 3; // Starting lives

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
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.linearVelocity = Vector2.down * projectileSpeed;
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D!");
            }
        }
    }

    // Call this function when the player takes damage
    public void TakeDamage(int damageAmount = 1) // Optional damage amount
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

    void Die()
    {
        Debug.Log("Player died!");
        // Add death effects, animations, etc.
        // Reload the current scene (or go to a game over screen)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload current scene
    }
}