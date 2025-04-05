using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolControler : MonoBehaviour
{
    public static ObjectPoolControler instance; // Clase singleton para manejar el Pool de objetos

    // Generamos la lista de objetos
    private List<GameObject> PoolPlayer = new List<GameObject>();
    // Objeto que guardaremos en el pool
    [SerializeField] GameObject AmmoPlayerPrefab;
    [SerializeField] int AmmoPlayerAmount; // Cantidad a generar

    // Generamos la lista de objetos
    private List<GameObject> PoolEnemy = new List<GameObject>();
    // Objeto que guardaremos en el pool
    [SerializeField] GameObject AmmoEnemyPrefab;
    [SerializeField] int AmmoEnemyAmount; // Cantidad a generar


    // Para convertirlo en Singleton
    void Awake()
    {
        // Garantizamos que solo exista una instancia del ObjectPool
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
    }

    // Método que se ejecuta antes del primer frame
    void Start()
    {
        // Generamos todos los objetos
        for (int i = 0; i < AmmoPlayerAmount; i++)
        {
            GameObject obj = Instantiate(AmmoPlayerPrefab); // Instanciamos el objeto
            obj.SetActive(false); // Lo desactivamos
            PoolPlayer.Add(obj); // Añadimos el objeto a la lista
        }

        // Generamos todos los objetos
        for (int i = 0; i < AmmoEnemyAmount; i++)
        {
            GameObject obj = Instantiate(AmmoEnemyPrefab); // Instanciamos el objeto
            obj.SetActive(false); // Lo desactivamos
            PoolEnemy.Add(obj); // Añadimos el objeto a la lista
        }
    }

    // --------------------------------------
    // MÉTODO GET POOLED OBJECT
    // --------------------------------------
    public GameObject GetPooledPlayer()
    {
        // Recorremos el pool
        for (int i = 0; i < PoolPlayer.Count; i++)
        {
            // Si el objeto no está activo en la jerarquía quiere decir
            // que está disponible para su uso
            if (!PoolPlayer[i].activeInHierarchy)
            {
                return PoolPlayer[i]; // Devolvemos el objeto disponible
            }
        }
        return null; // Si todos los objetos están ocupados devolvemos nulo
    }

    public GameObject GetPooledEnemy()
    {
        // Recorremos el pool
        for (int i = 0; i < PoolEnemy.Count; i++)
        {
            // Si el objeto no está activo en la jerarquía quiere decir
            // que está disponible para su uso
            if (!PoolEnemy[i].activeInHierarchy)
            {
                return PoolEnemy[i]; // Devolvemos el objeto disponible
            }
        }
        return null; // Si todos los objetos están ocupados devolvemos nulo
    }



}
