using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAmmo : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            this.gameObject.SetActive(false);
        }
    }
}
