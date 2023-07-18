using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MissileController : MonoBehaviour
{
   public Transform target;
    // Start is called before the first frame update
    public float speed = 5f;
    private Rigidbody2D rb;
    private Collider2D missileCollider;
    private Renderer missileRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        missileRenderer = GetComponent<Renderer>();
        missileCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (missileRenderer.isVisible)
        {
            // Calculate the direction from right to left
            Vector2 direction = new Vector2(-1f, 0f);

            // Normalize the direction
            direction = direction.normalized;

            // Convert speed from meters per second to units per second (assuming Unity's units are in meters)
            float speedInUnitsPerSecond = speed * Time.fixedDeltaTime;

            // Set the velocity of the missile using the modified direction and speed
            rb.velocity = direction * speedInUnitsPerSecond;
        }

    }
   
}
