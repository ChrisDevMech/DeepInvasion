using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] float LifeTime;
    [SerializeField] int damage;
    private void OnEnable()
    {
        StartCoroutine(AutoDisable(LifeTime));
    }

    IEnumerator AutoDisable(float seg)
    {
        yield return new WaitForSeconds(seg);
        this.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
