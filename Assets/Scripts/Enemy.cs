using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SplinePath splinePath;
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 8f;
    public float fireRate = 1f;
    public float rotationOffset = -90f; // Add this line
    public int health = 1;
    public int contactDamage = 1; // Damage dealt to player on contact

    private PowerUpSpawner powerUpSpawner; // Assign the PowerUpSpawner in the Inspector
    public GameObject deathParticlePrefab;
    public GameObject hitParticlePrefab;

    private float t = 0f;
    private Transform player;
    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        powerUpSpawner = GameObject.FindAnyObjectByType<PowerUpSpawner>();
    }

    void Update()
    {
        if (splinePath == null || splinePath.pathTransforms.Length < 2) return;

        if (t < 1f)
        {
            MoveAlongSpline();
        }
        else
        {
            Despawn();
        }

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void MoveAlongSpline()
    {
        Vector3 currentPosition = splinePath.GetPoint(t);
        transform.position = currentPosition;

        Vector3 nextPosition = splinePath.GetPoint(t + 0.01f);
        Vector3 direction = nextPosition - currentPosition;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset); // Add rotationOffset
        }

        t += moveSpeed * Time.deltaTime / splinePath.GetLength();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(contactDamage);
            }
        }
    }

    void Despawn()
    {      
        Destroy(gameObject);
    }

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null && fireRate > 0)
        {
            GameObject projectile = ObjectPoolControler.instance.GetPooledEnemy();
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                projectile.transform.position = projectileSpawnPoint.position;
                Vector2 direction = (player.position - projectileSpawnPoint.position).normalized;
                projectile.SetActive(true);
                projectileRb.linearVelocity = direction * projectileSpeed;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        health -= damage;
        if (health <= 0)
        {
             powerUpSpawner.SpawnPowerUp(transform.position); // Spawn power-up
            AudioController.instance.PlaySFX("DieEnemy");
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destroy the enemy

        }
    }

    public void SelectSpline(SplinePath path)
    {
        splinePath = path;
    }
}   