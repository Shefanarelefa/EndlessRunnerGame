using UnityEngine;
using System.Collections.Generic;

public class MasterSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public GameObject shieldPrefab;

    [Header("Targets")]
    public Transform player;

    [Header("Spawning Settings (Polished Casual Pacing)")]
    [Tooltip("Brings items closer into view so they don't look like they spawn too far away.")]
    public float spawnDistance = 35f;      
    
    [Tooltip("Controls how frequently rows spawn. Smaller number = more frequent items!")]
    public float rowSpacingZ = 18f;         

    private float nextRowZ;
    private float[] lanes = new float[] { -4f, -2f, 0f, 2f, 4f };
    private int rowsSinceLastShield = 0;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (player != null)
        {
            nextRowZ = player.position.z + spawnDistance;
        }
    }

    void Update()
    {
        if (player == null) return;

        // Triggers a new row pattern as the player drives forward
        if (player.position.z + spawnDistance > nextRowZ)
        {
            SpawnDesignPattern(nextRowZ);
            nextRowZ += rowSpacingZ;
        }
    }

    void SpawnDesignPattern(float zPosition)
    {
        rowsSinceLastShield++;
        
        // 1. Pick a random lane to be the "Hazard Lane" (0 to 4)
        int obstacleLane = Random.Range(0, 5);
        
        // Spawn the obstacle in that lane
        Vector3 obstaclePos = new Vector3(lanes[obstacleLane], 0.5f, zPosition);
        Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);

        // 2. Decide if we want a second obstacle to create a lane-switch challenge
        // We leave 3 lanes completely open so the player never gets trapped
        int secondObstacleLane = -1;
        if (Random.value < 0.5f)
        {
            secondObstacleLane = (obstacleLane + 2) % 5; // Safe distance away from first obstacle
            Vector3 secondObstaclePos = new Vector3(lanes[secondObstacleLane], 0.5f, zPosition);
            Instantiate(obstaclePrefab, secondObstaclePos, Quaternion.identity);
        }

        // 3. DESIGN INPUT: Spawn items based directly on where the obstacles are!
        for (int i = 0; i < lanes.Length; i++)
        {
            // Skip lanes that have obstacles in them
            if (i == obstacleLane || i == secondObstacleLane) continue;

            Vector3 itemPosition = new Vector3(lanes[i], 0.5f, zPosition);

            // If this lane is directly NEXT to the obstacle, it's the perfect high-reward lane!
            if (Mathf.Abs(i - obstacleLane) == 1)
            {
                // Force a Shield to spawn if we haven't seen one in 8 rows
                if (rowsSinceLastShield >= 8 && Random.value < 0.4f)
                {
                    Instantiate(shieldPrefab, itemPosition, Quaternion.identity);
                    rowsSinceLastShield = 0; // Reset counter
                }
                else
                {
                    // Otherwise, fill these adjacent paths heavily with coins!
                    Instantiate(coinPrefab, itemPosition, Quaternion.identity);
                }
            }
            else
            {
                // General lanes have a massive 60% chance to spawn coins now
                if (Random.value < 0.6f)
                {
                    Instantiate(coinPrefab, itemPosition, Quaternion.identity);
                }
            }
        }
    }
}