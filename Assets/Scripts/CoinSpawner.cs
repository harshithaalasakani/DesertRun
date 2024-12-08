using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;           // Assign your coin prefab in the Inspector
    public Transform playerTransform;       // Assign your player transform in the Inspector
    public float spawnDistance = 20f;       // Distance in front of the player to spawn coins
    public float laneWidth = 3f;            // Width of each lane (for multi-lane generation)
    public int maxCoins = 5;                // Maximum number of coins to spawn per set

    private List<GameObject> spawnedCoins = new List<GameObject>();  // List to track spawned coins

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            SpawnCoinSet();
            yield return new WaitForSeconds(2f);  // Adjust the spawn interval as needed
        }
    }

    void SpawnCoinSet()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;

        for (int i = 0; i < maxCoins; i++)
        {
            // Random lane position (left, center, or right)
            float laneOffset = Random.Range(-1, 2) * laneWidth;

            // Random height for variety (optional, adjust for flatter or varied spawn)
            float heightOffset = Random.Range(0.5f, 1.5f);

            Vector3 coinPosition = new Vector3(spawnPosition.x + laneOffset, heightOffset, spawnPosition.z + i * 2f);

            // Instantiate the coin at the spawn position with an upright rotation
            Quaternion uprightRotation = Quaternion.Euler(90f, 0f, 0f); // Adjust this depending on your model's default orientation
            GameObject newCoin = Instantiate(coinPrefab, coinPosition, uprightRotation);

            spawnedCoins.Add(newCoin);  // Add the spawned coin to the list
        }
    }


    public void ResetCoins()
    {
        // Destroy all spawned coins
        foreach (GameObject coin in spawnedCoins)
        {
            if (coin != null)
            {
                Destroy(coin);
            }
        }

        // Clear the list of coins
        spawnedCoins.Clear();
    }
}
