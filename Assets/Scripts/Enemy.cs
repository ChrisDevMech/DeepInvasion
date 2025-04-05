using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SplinePath splinePath;
    public float moveSpeed = 3f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 8f;
    public float fireRate = 1f;
    public bool autoShoot = true;

    private float t = 0f;
    private Transform player;
    private float nextFireTime;
    private bool despawned = false;

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
        Vector3 currentPosition = splinePath.GetPoint(t);
        transform.position = currentPosition;

        // Calculate the direction to the next point on the spline.
        Vector3 nextPosition = splinePath.GetPoint(t + 0.01f); // Look slightly ahead
        Vector3 direction = nextPosition - currentPosition;

        if (direction != Vector3.zero)
        {
            // Calculate the rotation to look in the direction of movement.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        t += moveSpeed * Time.deltaTime / splinePath.GetLength();
    }

    void Despawn()
    {
        despawned = true;
        Destroy(gameObject);
    }

    void Shoot()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
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
            else
            {
                Debug.LogError("Projectile prefab does not have a Rigidbody2D!");
            }
        }
    }
}   