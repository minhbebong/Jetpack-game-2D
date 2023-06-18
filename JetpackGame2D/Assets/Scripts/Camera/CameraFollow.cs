using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject mouseObject;
    private float distanceTarget;
    void Start()
    {
        distanceTarget = transform.position.x - mouseObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseObjectX = mouseObject.transform.position.x;
        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x = mouseObjectX + distanceTarget;
        transform.position = newCameraPosition;
    }
}
