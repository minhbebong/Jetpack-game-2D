using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxController : MonoBehaviour
{
    public Renderer background;
    public Renderer foreground;

    public float backgroundSpeed = 0.02f;
    public float foregroundSpeed = 0.06f;

    public float offset = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float backgroundOffset = offset * backgroundSpeed;
        float foregroundOffset = offset * foregroundSpeed;

        background.material.mainTextureOffset = new Vector2(backgroundOffset, 0);
        background.material.mainTextureOffset = new Vector2(foregroundOffset, 0);
    }
}