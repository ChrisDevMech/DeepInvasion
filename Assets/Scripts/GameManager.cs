using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject fondo;
    [SerializeField] GameObject Boss;
    private float offset = 0f;

    public void Start()
    {
        StartCoroutine(AnimationBackground());
    }

    IEnumerator AnimationBackground()
    {
        float startTime = Time.time; // Almacena el tiempo de inicio de la corrutina
        float duration = 65f; // Duración en segundos

        while (Time.time - startTime < duration) // Bucle while hasta que pasen 65 segundos
        {
            offset += -0.01f * Time.deltaTime;
            fondo.GetComponent<SpriteRenderer>().material.mainTextureOffset = new Vector2(0, offset);
            StartCoroutine(BossAppear());
            yield return null;
        }
    }

    IEnumerator BossAppear()
    {
        Instantiate(Boss);
        yield return null;
    }

}
