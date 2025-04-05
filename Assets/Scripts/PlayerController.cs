using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;
    public int lives = 3;
    public bool isPoweredUp = false;
    public float powerUpTimer = 0f;
    public float powerUpDuration = 10f;

    public Transform leftCannonSpawnPoint; // Assign in Inspector
    public Transform rightCannonSpawnPoint; // Assign in Inspector

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
        if (leftCannonSpawnPoint == null || rightCannonSpawnPoint == null)
        {
            Debug.LogError("Cannon Spawn Points not assigned!");
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        rb.linearVelocity = movement * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = ObjectPoolControler.instance.GetPooledPlayer();
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectile.transform.position = projectileSpawnPoint.position;
                projectile.SetActive(true);
                projectileRb.linearVelocity = Vector2.down * projectileSpeed;

                if (isPoweredUp)
                {
                    ShootDiagonalCannons();
                }
            }
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D!");
            }
        }
    }

    void ShootDiagonalCannons()
    {
        if (projectilePrefab != null && leftCannonSpawnPoint != null && rightCannonSpawnPoint != null)
        {
            GameObject leftProjectile = ObjectPoolControler.instance.GetPooledPlayer();
            Rigidbody2D leftProjectileRb = leftProjectile.GetComponent<Rigidbody2D>();
            Debug.Log(leftProjectile);
            Debug.Log(leftProjectileRb);

            GameObject rightProjectile = ObjectPoolControler.instance.GetPooledPlayer();
            Rigidbody2D rightProjectileRb = rightProjectile.GetComponent<Rigidbody2D>();
            if (leftProjectileRb != null && rightProjectileRb != null)
            {
                Debug.Log("pew"); 
                rightProjectile.transform.position = leftCannonSpawnPoint.position;
                rightProjectile.SetActive(true);
                rightProjectileRb.linearVelocity = new Vector2(-1, -1).normalized * projectileSpeed;

                leftProjectile.transform.position = rightCannonSpawnPoint.position;
                leftProjectile.SetActive(true);
                leftProjectileRb.linearVelocity = new Vector2(1, -1).normalized * projectileSpeed;
            }
            else
            {
                Debug.LogError("Diagonal projectile prefabs do not have a Rigidbody2D!");
            }
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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