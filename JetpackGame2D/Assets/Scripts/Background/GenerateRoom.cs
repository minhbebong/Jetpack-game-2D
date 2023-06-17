using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoom : MonoBehaviour
{
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    public float screenWidthInPoints;

    public GameObject[] availableObjects;
    public List<GameObject> objects;
    // Start is called before the first frame update
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
