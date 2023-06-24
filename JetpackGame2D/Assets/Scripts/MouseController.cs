using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    public float forwardMovementSpeed = 3.0f;
    public float jetpackForce = 75f;
    private Rigidbody2D playerbody;
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
        Vector3 pos = transform.position;
        pos.x += forwardMovementSpeed * Time.deltaTime;
        transform.position = pos;
    }
}
