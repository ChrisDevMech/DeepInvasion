using System.Collections;
using TMPro;
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
            yield return null;
        }
        StartCoroutine(BossAppear());
        yield return null;
    }

    IEnumerator BossAppear()
    {
        Vector2 startPosition = new Vector2(0, -20);
        Vector2 targetPosition = new Vector2(0, -10);
        GameObject bossInstance = Instantiate(Boss, startPosition, Quaternion.identity);

        float startTime = Time.time;
        float journeyLength = Vector2.Distance(startPosition, targetPosition);

        while (bossInstance.transform.position.y != targetPosition.y)
        {
            float distanceCovered = (Time.time - startTime) * 5f;
            float fractionOfJourney = distanceCovered / journeyLength;
            bossInstance.transform.position = Vector2.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }
        yield return null;
    }

}
