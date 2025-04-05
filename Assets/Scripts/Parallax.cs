using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Velocidad de desplazamiento de la capa
    public float velocidadParallax;
    // La c·mara que estÅEactiva
    public GameObject camara;
    //PosiciÛn del fondo del parallax
    private Vector3 posicionInicial;
    private float anchoImagen;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>(); // Obtenemos el SpriteRenderer del fondo
        anchoImagen = renderer.bounds.size.y; // Obtenemos el largo del fondo
    }

    // Update is called once per frame
    void Update()
    {
        // Movemos la capa en funciÛn de la pos. actual de la c·mara y la vel. del parallax
        float desplazamiento = camara.transform.position.y * velocidadParallax;
        // Aplicamos el nuevo posicionamiento del fondo
        transform.position = (Vector2)posicionInicial + (Vector2.down * desplazamiento);
        // -----------------------------------------------------------------------------------------------------------------
        // Verificamos si la c·mara ha llegado al borde de la imagen
        if (camara.transform.position.y - transform.position.y >= anchoImagen)
        {
            Debug.Log("Me teletransporto");
            posicionInicial += new Vector3(0 , 1f * 2, 0); // Reposicionamos la capa para que parezca un fondo infinito
        }
    }
}