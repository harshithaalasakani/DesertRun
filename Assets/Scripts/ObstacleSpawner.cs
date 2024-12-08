using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;     // Array to store multiple obstacle prefabs
    public Transform playerTransform;        // Reference to the player’s transform
    public float spawnDistance = 30f;        // Distance in front of the player to spawn obstacles
    public float laneWidth = 3f;             // Width of each lane (for multi-lane generation)
    public int maxObstacles = 3;             // Max number of obstacles per spawn set
    public float spawnInterval = 3f;         // Time interval between obstacle spawns

    void Start()
    {
        if (obstaclePrefabs.Length == 0)
        {
            Debug.LogError("No obstacles assigned to the array. Please assign obstacle prefabs.");
            return; // Stop further execution if no obstacles are assigned
        }

        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacleSet();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacleSet()
    {
        Vector3 spawnPosition = playerTransform.position + Vector3.forward * spawnDistance;

        for (int i = 0; i < maxObstacles; i++)
        {
            // Random lane position (left, center, or right)
            float laneOffset = Random.Range(-1, 2) * laneWidth;

            // Slight random height for varied spawn (optional)
            float heightOffset = Random.Range(0.5f, 1.5f);

            // Randomly pick an obstacle from the array
            int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);

            // Get the selected obstacle prefab
            GameObject selectedObstacle = obstaclePrefabs[randomObstacleIndex];

            // Position for the obstacle
            Vector3 obstaclePosition = new Vector3(spawnPosition.x + laneOffset, heightOffset, spawnPosition.z + i * 3f);

            // Instantiate the selected obstacle
            Instantiate(selectedObstacle, obstaclePosition, Quaternion.identity);
        }
    }
}
