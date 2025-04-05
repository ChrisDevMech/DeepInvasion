using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public int cameraSpeed;

    // Update is called once per frame
    void Update()
    {

        // Detectamos si el desplazamiento es hacia la derecha
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            // Muevo la cámara a la derecha
            transform.position = transform.position + (Vector3.down * cameraSpeed * Time.deltaTime);
        }

    }
}
