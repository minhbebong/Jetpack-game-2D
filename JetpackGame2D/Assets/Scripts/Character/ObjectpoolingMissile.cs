using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectpoolingMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public int poolSize = 3;
    public Transform player;
    private List<GameObject> missilePool;

    public float spawnInterval = 10f; // Th?i gian gi?a m?i l?n xu?t hi?n missile
    private int currentMissileIndex = 0; // Keep track of the current missile index to spawn

    private void Start()
    {
        missilePool = new List<GameObject>();

        // Fill the missile pool with initial missiles
        for (int i = 0; i < poolSize; i++)
        {
            GameObject missile = Instantiate(missilePrefab);
            missile.SetActive(false);
            missilePool.Add(missile);
        }

        // Spawn the missiles after a delay
        InvokeRepeating("SpawnMissile", spawnInterval, spawnInterval);
    }

    private void SpawnMissile()
    {
        // Find the next inactive missile from the pool
        GameObject missile = GetInactiveMissile();

        if (missile != null)
        {
            Vector3 spawnPosition = new Vector3(Screen.width, Random.Range(0, Screen.height), 0);
            spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
            spawnPosition.z = 0;
            missile.transform.position = spawnPosition;

            MissileController missileController = missile.GetComponent<MissileController>();
            missileController.target = player; // Assign the player character's Transform to the missile's target
            missile.SetActive(true);
        }
    }
    private GameObject GetInactiveMissile()
    {
        // Find an inactive missile from the pool
        for (int i = 0; i < missilePool.Count; i++)
        {
            int indexToCheck = (currentMissileIndex + i) % missilePool.Count; // Wrap around to the beginning if necessary
            if (!missilePool[indexToCheck].activeInHierarchy)
            {
                currentMissileIndex = (indexToCheck + 1) % missilePool.Count; // Set the next index for the next missile spawn
                return missilePool[indexToCheck];
            }
        }

        // If no inactive missiles are found, return null
        return null;
    }
}



