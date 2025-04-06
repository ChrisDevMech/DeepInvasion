using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject fondo;
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
    }

}
