using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public SplinePath splinePath;
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 8f;
    public float fireRate = 1f;
    public bool autoShoot = true;
    public int lives = 1; // Starting lives

    private float t = 0f;
    private Transform player;
    private float nextFireTime;
    private bool despawned = false; // Flag to indicate if the enemy has despawned

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (splinePath == null || splinePath.pathPoints.Length < 2 || despawned) return;

        if (t < 1f)
        {
            MoveAlongSpline();
        }
        else
        {
            Despawn();
        }

        if (autoShoot && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void MoveAlongSpline()
    {
        transform.position = splinePath.GetPoint(t);
        t += moveSpeed * Time.deltaTime / splinePath.GetLength();
    }

    void Despawn()
    {
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                Vector2 direction = (player.position - projectileSpawnPoint.position).normalized;
                projectileRb.linearVelocity = direction * projectileSpeed;
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
            Debug.Log("Enemy died!");
            Despawn();
        }
        else
        {
            Debug.Log("Enemy took damage. Lives remaining: " + lives);
        }
    }
}
