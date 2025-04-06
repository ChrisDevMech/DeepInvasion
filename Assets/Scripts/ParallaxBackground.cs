using System.Collections;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeed = 0.1f; // Adjust this for parallax speed

    private float offset = 0f;

    void Update()
    {
        offset += parallaxSpeed * Time.deltaTime;
        this.gameObject.GetComponent<SpriteRenderer>().material.mainTextureOffset = new Vector2(0, offset);
    }

    
}