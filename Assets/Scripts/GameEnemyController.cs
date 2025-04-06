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

    //Variables para controlar cantidad y frecuencia
    [SerializeField] int AmountWE, AmountSE;
    [SerializeField] float FrecuenceWE, FrecuenceSE;

    public void Start()
    {
        StartCoroutine(SpawnWE(FrecuenceWE,AmountWE));
        StartCoroutine(SpawnSE(FrecuenceSE,AmountSE));
    }

    IEnumerator SpawnWE(float seg, int amount)
    {
        yield return new WaitForSeconds(1f);
        // Selecciona un SplinePath aleatorio de la lista
        for (int i = 0; i < amount; i++)
        {     
            int randomIndex = Random.Range(0, ListSpawWE.Count);
            SplinePath selectedSpline = ListSpawWE[randomIndex];
            // Spawnea el WeakEnemy en la posición inicial del SplinePath
            GameObject newWE = Instantiate(WeakEnemy, new Vector2(0, -10), Quaternion.identity);
            newWE.GetComponent<Enemy>().SelectSpline(selectedSpline);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(seg);
        StartCoroutine(SpawnWE(seg, amount));
    }
    IEnumerator SpawnSE(float seg, int amount)
    {
        yield return new WaitForSeconds(1f);
        // Selecciona un SplinePath aleatorio de la lista
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, ListSpawSE.Count);
            SplinePath selectedSpline = ListSpawSE[randomIndex];
            // Spawnea el StrongEnemy en la posición inicial del SplinePath
            GameObject newSE = Instantiate(StrongEnemy, new Vector2(0, -10), Quaternion.identity);
            newSE.GetComponent<Enemy>().SelectSpline(selectedSpline);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(seg);
        StartCoroutine(SpawnWE(seg, amount));
    }
    IEnumerator SpawnBoss(float seg, int amount)
    {
        yield return new WaitForSeconds(1f);
        // Selecciona un SplinePath aleatorio de la lista

        // Spawnea el Boss en la posición inicial del SplinePath
        GameObject newBoss = Instantiate(Boss, new Vector2(0,-10), Quaternion.identity);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(seg);
        StartCoroutine(SpawnWE(seg, amount));
    }


}
