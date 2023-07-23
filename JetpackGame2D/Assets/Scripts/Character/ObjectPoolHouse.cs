using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolHouse : MonoBehaviour
{
    public List<GameObject> housePrefabs; 
    public int poolSizePerHouse = 10; 
    private List<List<GameObject>> coinPools; // List of coin pools

    public float spawnInterval = 2f; // Time between each coin spawn
    private Transform player;

    private List<int> currentCoinIndices; // Keep track of the current coin index to spawn for each coin prefab

    private void Start()
    {
        coinPools = new List<List<GameObject>>();
        currentCoinIndices = new List<int>();

        // Create a pool for each coin prefab
        for (int i = 0; i < housePrefabs.Count; i++)
        {
            List<GameObject> coinPool = new List<GameObject>();
            for (int j = 0; j < poolSizePerHouse; j++)
            {
                GameObject coin = Instantiate(housePrefabs[i]);
                coin.SetActive(false);
                coinPool.Add(coin);
            }
            coinPools.Add(coinPool);
            currentCoinIndices.Add(0); // Initialize the current coin index for each coin prefab
        }

        // Set the player reference
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Spawn the coins after a delay
        InvokeRepeating("SpawnCoin", spawnInterval, spawnInterval);
    }

    private void SpawnCoin()
    {
        // Find the next inactive coin from the pool for each coin prefab
        for (int i = 0; i < coinPools.Count; i++)
        {
            GameObject coin = GetInactiveCoin(i);
            if (coin != null)
            {
                Vector3 spawnPosition = new Vector3(Screen.width, Random.Range(0, Screen.height), 0);
                spawnPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
                spawnPosition.z = 0;
                coin.transform.position = spawnPosition;

                // Set any additional properties or behavior for the coin if needed

                coin.SetActive(true);
            }
        }
    }

    private GameObject GetInactiveCoin(int coinIndex)
    {
        // Find an inactive coin from the pool for the specified coin prefab
        List<GameObject> coinPool = coinPools[coinIndex];
        int currentIndex = currentCoinIndices[coinIndex];

        for (int i = 0; i < coinPool.Count; i++)
        {
            int indexToCheck = (currentIndex + i) % coinPool.Count; // Wrap around to the beginning if necessary
            if (!coinPool[indexToCheck].activeInHierarchy)
            {
                currentCoinIndices[coinIndex] = (indexToCheck + 1) % coinPool.Count; // Set the next index for the next coin spawn
                return coinPool[indexToCheck];
            }
        }

        // If no inactive coins are found for the specified coin prefab, return null
        return null;
    }
}
