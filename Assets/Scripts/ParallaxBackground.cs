using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeed = 0.1f; // Adjust this for parallax speed
    public Renderer backgroundRenderer; // Assign the renderer of your background sprite

    private float offset = 0f;

    void Start()
    {
        if (backgroundRenderer == null)
        {
            backgroundRenderer = GetComponent<Renderer>(); // Attempt to get the renderer if not assigned.
            if (backgroundRenderer == null)
            {
                Debug.LogError("No renderer assigned for ParallaxBackground!");
            }
        }
    }

    void Update()
    {
        offset += parallaxSpeed * Time.deltaTime;
        backgroundRenderer.material.mainTextureOffset = new Vector2(0, offset);
    }
}