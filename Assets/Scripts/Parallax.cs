using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float distanciaTotal;

    private Vector2 posicionInicial;
    private float distanciaRecorrida = 0.0f;
    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (distanciaRecorrida < distanciaTotal)
        {
            float desplazamiento = velocidad * Time.deltaTime;
            transform.Translate(Vector2.up * desplazamiento);
            distanciaRecorrida += desplazamiento;
        }
    }
}