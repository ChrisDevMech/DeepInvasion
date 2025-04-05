using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyController : MonoBehaviour
{
    // Prefabs de los enemigos del nivel
    [SerializeField] GameObject WeakEnemy;
    [SerializeField] GameObject StrongEnemy;
    [SerializeField] GameObject Boss;

    // Generamos la lista de objetos
    [SerializeField] private List<SplinePath> ListSpawWE = new List<SplinePath>();
    [SerializeField] private List<SplinePath> ListSpawSE = new List<SplinePath>();
    [SerializeField] private List<SplinePath> ListSpawBoss = new List<SplinePath>();

    //Variables para controlar cantidad y frecuencia
    [SerializeField] int AmountWE, AmountSE, AmountBoss;
    [SerializeField] float FrecuenceWE, FrecuenceSE, FrecuenceBoss;

    public void Start()
    {
        StartCoroutine(SpawnWE(FrecuenceWE,AmountWE));
    }

    IEnumerator SpawnWE(float seg, int amount)
    {
        if (ListSpawWE.Count == 0)
        {
            Debug.LogWarning("La lista de SplinePath para WeakEnemy está vacía.");
        }
        // Selecciona un SplinePath aleatorio de la lista
        for (int i = 0; i < amount; i++)
        {     
            int randomIndex = Random.Range(0, ListSpawWE.Count);
            SplinePath selectedSpline = ListSpawWE[randomIndex];
            // Spawnea el WeakEnemy en la posición inicial del SplinePath
            GameObject newWE = Instantiate(WeakEnemy, selectedSpline.transform.position, Quaternion.identity);
            newWE.GetComponent<Enemy>().SelectSpline(selectedSpline);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(seg);
        StartCoroutine(SpawnWE(seg, amount));
    }
    IEnumerator SpawnSE(float seg)
    {
        yield return new WaitForSeconds(seg);
    }
    IEnumerator SpawnBoss(float seg)
    {
        yield return new WaitForSeconds(seg);
    }


}
