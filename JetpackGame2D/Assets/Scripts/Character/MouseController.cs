using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    public float forwardMovementSpeed = 3.0f;
    public float jetpackForce = 75f;
    private Rigidbody2D playerbody;
    private bool isDead = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        HitByLaser(collider);
    }

    void HitByLaser(Collider2D laserCollider)
    {
        isDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jetpackActive = Input.GetButton("Fire1");
        if (jetpackActive)
        {
            playerbody.AddForce(new Vector2(0, jetpackForce));
        }
        
    }

    private void FixedUpdate()
    {
        //Vector3 pos = transform.position;
        //pos.x += forwardMovementSpeed * Time.deltaTime;
        //transform.position = pos;
        bool jetpackActive = Input.GetButton("Fire1");
        jetpackActive = jetpackActive && !isDead;

        if (jetpackActive)
        {
            playerbody.AddForce(new Vector2(0, jetpackForce));
        }

        if (!isDead)
        {
            Vector2 newVelocity = playerbody.velocity;
            newVelocity.x = forwardMovementSpeed;
            playerbody.velocity = newVelocity;
        }
        
        UpdateGroundedStatus();
        AdjustJetpack(jetpackActive);
    }

    void UpdateGroundedStatus()
    {
        // TODO: Implement your logic for updating grounded status here
    }

    void AdjustJetpack(bool jetpackActive)
    {
        // TODO: Implement your logic for adjusting the jetpack here
    }
}
