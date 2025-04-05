using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class Player : MonoBehaviour {
    //EVENTO (DELEGADO)   --> Effect Spawn
    public delegate void PlayerFXSpawn(Vector3 position);
    public static event PlayerFXSpawn onPlayerFXSpawn;    //(EVENTO)
    //EVENTO (DELEGADO)   --> Effect Destroy
    public delegate void PlayerFXDestroy(Vector3 position);
    public static event PlayerFXDestroy onPlayerFXDestroy;    //(EVENTO)


    [Header("Lives")]
    [SerializeField] int maxLive = 3;
    [SerializeField] int currentLive;

    [Header("Gun")]
    [SerializeField] List<Weapon> weaponList;
    //[SerializeField] List<Bullet> bulletList;
    [SerializeField] int maxAmmo = 3;
    [SerializeField] int currentAmmo;

    public float fireRate = 0.8f; // Tiempo entre disparos
    private float nextFireTime = 0f; // Tiempo del próximo disparo


    //SUSCRIPCIÓN al EVENTO
    void OnEnable() {
        PlayerController.onPlayerFire += OnPlayerFire;
    }
    //DESUSCRIPCIÓN al EVENTO
    void OnDisable() {
        PlayerController.onPlayerFire -= OnPlayerFire;
    }

    private void Start() {
        currentLive = maxLive;
        currentAmmo = maxAmmo;

        //Evento Effect Spawn
        if (onPlayerFXSpawn != null)
            onPlayerFXSpawn(transform.position);
    }

    public void SetDestroyed() {
        //Sound Destroy
        AudioManager.instance.PlaySFX("ShipDestroy");
        //Evento Effect Destroy
        if (onPlayerFXDestroy != null)
            onPlayerFXDestroy(transform.position);

        Destroy(gameObject);
    }

    private void OnPlayerFire() {
        Debug.Log("Fire");
        // Disparo
        if (currentAmmo > 0 && weaponList != null && Time.time > nextFireTime) {
            // Itera a través de todos los cañones y dispara
            foreach (Weapon weapon in weaponList) {
                weapon.Fire();
            }

            //Resta una bala
            LessAmmo();

            // Actualiza el tiempo del próximo disparo
            nextFireTime = Time.time + fireRate;
        }
    }

    public int GetCurrentLive() {
        return currentLive;
    }
    public void LessLive() {
        currentLive--;
        if (currentLive <= 0) {
            SetDestroyed();
        }
    }
    public void AddLive() {
        if (currentLive < maxLive)
            currentLive++;
    }

    public int GetCurrentAmmo() {
        return currentAmmo;
    }
    public void LessAmmo() {
        currentAmmo--;
    }
    public void FullAmmo() {
        currentAmmo = maxAmmo;
    }
}
