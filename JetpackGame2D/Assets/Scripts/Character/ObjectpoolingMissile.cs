using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectpoolingMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public int poolSize = 3;
    public Transform player;
    private List<GameObject> missilePool;
  
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
        InvokeRepeating("SpawnMissile", 5f, 5f);
    }

    private void SpawnMissile()
    {
        // Find an inactive missile from the pool
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
            if (!missilePool[i].activeInHierarchy)
            {
                return missilePool[i];
            }
        }

        // If no inactive missiles are found, create a new one and add it to the pool
        GameObject newMissile = Instantiate(missilePrefab);
        newMissile.SetActive(false);
        missilePool.Add(newMissile);

        return newMissile;
    }
}
    

