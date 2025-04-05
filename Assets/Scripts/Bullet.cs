using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Bullet : MonoBehaviour {
    //EVENTO (DELEGADO)   --> activar el Efecto Fx al Disparar
    public delegate void BulletFXSpawn(Vector3 position);
    public static event BulletFXSpawn onBulletFXSpawn;  //(EVENTO)
    //EVENTO (DELEGADO)   --> activar el Efecto Fx al Destruirse
    public delegate void BulletFXDestroy(Vector3 position);
    public static event BulletFXDestroy onBulletFXDestroy;  //(EVENTO)

    [SerializeField] float speed;

    [SerializeField] Rigidbody2D rigidbody;

    private Enemy enemy;
    private Player player;
    private Bullet bullet;

    public void Start() {
        rigidbody.velocity = transform.right * speed;

        //Sonido Disparo
        AudioManager.instance.PlaySFX("BulletShoot");
        //Evento Efecto Spawn
        if (onBulletFXSpawn != null)
            onBulletFXSpawn(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Comprueba si la colisi�n es con un jugador, un enemigo o una bala
        enemy = other.GetComponent<Enemy>();
        player = other.GetComponent<Player>();
        bullet = other.GetComponent<Bullet>();

        if (enemy != null && (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss"))) {
            Debug.Log("Contact ENEMY");
            enemy.LessLive();               //Quita una vida
            SetDestroyedWithEffects();         //La bala se autodestruye
        }

        if (player != null) {
            Debug.Log("Contact PLAYER");
            player.LessLive();          //Quita una vida
            SetDestroyedWithEffects();    //La bala se autodestruye
        }

        if (bullet != null) {
            Debug.Log("Contact BULLET");
            SetDestroyedWithEffects();         //La bala se autodestruye
        }

        // Si choca con una piedra se destruye
        if (other.CompareTag("Wall")) {
            SetDestroyedWithEffects();    //La bala se autodestruye
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Player>() != null)
        {
            Debug.Log("Exit");
            SetDestroyedWithEffects();
        }
    }

    public void SetDestroyedWithEffects() {
        //Sonido Destroy
        AudioManager.instance.PlaySFX("BulletDestroy");
        //Evento Efecto Destroy
        if (onBulletFXDestroy != null)   //Le sumo este valor para que el efecto toque con la pared
            onBulletFXDestroy(transform.position);

        Destroy(gameObject);    //La bala se autodestruye
    }

    public void SetDestroyed() {
        Destroy(gameObject);         //La bala se autodestruye
    }
}