using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GenerateRoom : MonoBehaviour
{
    public GameObject[] availableObjects;
    public List<GameObject> objects;
    public List<GameObject> currentRooms;

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    public float objectsMinRotation = -45.0f;
    public float objectsMaxRotation = 45.0f;

    public GameObject[] availableRooms;

    public float screenWidthInPoints;

    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
    }

    void FixedUpdate()
    {
        GenerateRoomIfRequired();
        GenerateObjectsIfRequired();
    }

    void AddRoom(float farhtestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
        float roomWidth = room.transform.Find("floor").localScale.x;
        float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);
    }

    void GenerateRoomIfRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farthestRoomEndX = 0;

        foreach (var room in currentRooms)
        {
            float roomWidth = room.transform.Find("floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;

            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }

            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }

            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            room.SetActive(false);
        }

        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }

        GenerateObjectsIfRequired();
    }

    void AddObject(float lastObjectX)
    {
        if (availableObjects.Length == 0)
        {
            Debug.LogWarning("Không có đối tượng trong mảng availableObjects.");
            return;
        }

        int randomIndex = Random.Range(0, availableObjects.Length);
        if (randomIndex >= 0 && randomIndex < availableObjects.Length)
        {
            GameObject obj = Instantiate(availableObjects[randomIndex]);
            float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
            float randomY = Random.Range(objectsMinY, objectsMaxY);
            obj.transform.position = new Vector3(objectPositionX, randomY, 0);
            float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
            obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
            objects.Add(obj);
        }
        else
        {
            Debug.LogWarning("randomIndex không hợp lệ khi lựa chọn availableObjects.");
        }
    }

    void GenerateObjectsIfRequired()
    {
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;

        List<GameObject> objectsToRemove = new List<GameObject>();
        foreach (var obj in objects)
        {
            if (obj != null && obj.activeSelf)
            {
                float objX = obj.transform.position.x;
                farthestObjectX = Mathf.Max(farthestObjectX, objX);

                if (objX < removeObjectsX)
                {
                    objectsToRemove.Add(obj);
                }
            }
            else
            {
                objectsToRemove.Add(obj);
            }
        }

        foreach (var obj in objectsToRemove)
        {
            objects.Remove(obj);
            if (obj != null && obj is GameObject)
            {
                //Destroy(obj)
                obj.SetActive(false);
            }
        }

        if (farthestObjectX < addObjectX)
        {
            AddObject(farthestObjectX);
        }
    }
}
