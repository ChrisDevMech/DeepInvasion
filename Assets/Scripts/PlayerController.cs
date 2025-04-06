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
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite defaultSprite;

    public Transform leftCannonSpawnPoint; // Assign in Inspector
    public Transform rightCannonSpawnPoint; // Assign in Inspector

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize SpriteRenderer
        if (defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        rb.linearVelocity = movement * moveSpeed;

        // Change sprite based on horizontal movement
        if (horizontalInput > 0)
        {
            if (rightSprite != null)
            {
                spriteRenderer.sprite = rightSprite;
            }
        }
        else if (horizontalInput < 0)
        {
            if (leftSprite != null)
            {
                spriteRenderer.sprite = leftSprite;
            }
        }
        else
        {
            if (defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }


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
        }
    }

    void ShootDiagonalCannons()
    {
        if (projectilePrefab != null && leftCannonSpawnPoint != null && rightCannonSpawnPoint != null)
        {
            GameObject leftProjectile = ObjectPoolControler.instance.GetPooledPlayer();
            Rigidbody2D leftProjectileRb = leftProjectile.GetComponent<Rigidbody2D>();

            leftProjectile.transform.position = rightCannonSpawnPoint.position;
            leftProjectile.SetActive(true);
            leftProjectileRb.linearVelocity = new Vector2(1, -1).normalized * projectileSpeed;


            GameObject rightProjectile = ObjectPoolControler.instance.GetPooledPlayer();
            Rigidbody2D rightProjectileRb = rightProjectile.GetComponent<Rigidbody2D>();

            rightProjectile.transform.position = leftCannonSpawnPoint.position;
            rightProjectile.SetActive(true);
            rightProjectileRb.linearVelocity = new Vector2(-1, -1).normalized * projectileSpeed;

        }
    }

    void Die()
    {
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
    }
}