using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;
    public Transform[] projectileSpawnPoints; // Use an array of spawn points
    public float projectileSpeed = 4f;
    public float fireRate = 1f;
    public float rotationOffset = -90f; // Add this line
    public int health = 1;
    public int contactDamage = 2; // Damage dealt to player on contact

    private float t = 0f;
    private Transform player;
    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
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

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoints != null && projectileSpawnPoints.Length > 0 && fireRate > 0)
        {
            foreach (Transform spawnPoint in projectileSpawnPoints)
            {
                if (spawnPoint != null)
                {
                    GameObject projectile = ObjectPoolControler.instance.GetPooledEnemy();
                    Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

                    if (projectileRb != null)
                    {
                        projectile.transform.position = spawnPoint.position;
                        Vector2 direction = (player.position - spawnPoint.position).normalized;
                        projectile.SetActive(true);
                        projectileRb.linearVelocity = direction * projectileSpeed;
                    }
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("oof");
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("ded");
            AudioController.instance.PlaySFX("DieEnemy");
            Destroy(gameObject); // Destroy the enemy
        }
    }

}