using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //EVENTO (DELEGADO)   --> Effect Spawn
    public delegate void EnemyFXSpawn(Vector3 position);
    public static event EnemyFXSpawn onEnemyFXSpawn;    //(EVENTO)
    //EVENTO (DELEGADO)   --> Effect Destroy
    public delegate void EnemyFXDestroy(Vector3 position);
    public static event EnemyFXDestroy onEnemyFXDestroy;    //(EVENTO)

    [Header("Lives")]
    [SerializeField] int maxLive = 3;
    [SerializeField] int currentLive;

    [Header("Gun")]
    [SerializeField] List<Weapon> weaponList;
    //[SerializeField] List<Bullet> bulletList;

    public float fireRate = 1f; // Tiempo entre disparos
    private float nextFireTime = 0f; // Tiempo del próximo disparo

    private void Start() {
        currentLive = maxLive;

        //Evento Effect Spawn
        if (onEnemyFXSpawn != null)
            onEnemyFXSpawn(transform.position);
    }

    public void SetDestroyed() {
        //Sound Destroy
        AudioManager.instance.PlaySFX("ShipDestroy");
        //Evento Effect Destroy
        if (onEnemyFXDestroy != null)
            onEnemyFXDestroy(transform.position);

        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }

    public int GetCurrentHealth() {
        return currentLive;
    }

    public int GetMaxHealth() {
        return maxLive;
    }

    public void OnEnemyFire() {
        // Disparo
        if (weaponList != null && Time.time > nextFireTime) {
            Debug.Log("Enemy Fire");
            // Itera a través de todos los cañones y dispara
            foreach (Weapon weapon in weaponList) {
                weapon.Fire();
            }

            // Actualiza el tiempo del próximo disparo
            nextFireTime = Time.time + fireRate;
        }
    }

    public void LessLive() {
        currentLive--;
        if (currentLive <= 0) {
            SetDestroyed();
        }
    }
}
