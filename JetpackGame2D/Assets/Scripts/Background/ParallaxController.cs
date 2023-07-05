using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxController : MonoBehaviour
{
    public SpriteRenderer parallaxBackground;
    public SpriteRenderer parallaxForceground;
    public float parallaxSpeed;
    internal float offset;

    public Renderer background;
    public Renderer foreground;

    void Start()
    {
        
    }

    void Update()
    {
      // Get the current position of the backgrounds
    Vector3 pos1 = parallaxBackground.transform.position;
        Vector3 pos2 = parallaxForceground.transform.position;

        // Calculate the new position based on the parallax speed and time
        float offset = Time.time * parallaxSpeed;
        float newPos1 = pos1.x + offset;
        float newPos2 = pos2.x + offset;

        // Set the new position of the backgrounds
        parallaxBackground.transform.position = new Vector3(newPos1, pos1.y, pos1.z);
        parallaxForceground.transform.position = new Vector3(newPos2, pos2.y, pos2.z);
    }
}
