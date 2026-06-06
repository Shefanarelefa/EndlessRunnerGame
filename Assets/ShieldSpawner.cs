using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [Header("Prefabs & Targets")]
    public GameObject shieldPrefab;
    public Transform player;

    [Header("Spawning Settings")]
    public float spawnDistance = 30f; // How far ahead of the player to spawn
    public float spacingZ = 40f;      // Distance between consecutive shield spawns

    private float nextZ;
    private float[] lanes = new float[] { -4f, -2f, 0f, 2f, 4f };

    void Start()
    {
        // Automatically find the player if it wasn't assigned in the inspector
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) 
                player = p.transform;
        }

        if (player != null)
        {
            // Set the first spawn position relative to where the player starts
            nextZ = player.position.z + spawnDistance;
        }
        else
        {
            Debug.LogError("❌ Player NOT assigned and could not be found by Tag 'Player' in ShieldSpawner!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Check if the player has advanced far enough to trigger the next spawn
        if (player.position.z + spawnDistance > nextZ)
        {
            SpawnShield(nextZ);
            nextZ += spacingZ; // Schedule the next spawn distance
        }
    }

    void SpawnShield(float z)
    {
        if (shieldPrefab == null)
        {
            Debug.LogError("❌ Shield Prefab NOT assigned in the ShieldSpawner Inspector!");
            return;
        }

        // Randomly choose one of the 5 lanes (indices 0 to 4)
        int laneIndex = Random.Range(0, 5);
        float x = lanes[laneIndex];
        
        // Match the 0.5f height you use for coins and magnets
        Vector3 spawnPos = new Vector3(x, 0.5f, z);

        // SAFE CHECK: Ensure we aren't dropping the shield directly inside an obstacle box
        Collider[] hits = Physics.OverlapSphere(spawnPos, 1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Obstacle"))
            {
                Debug.Log("❌ Shield spawn blocked by an obstacle at Z: " + z);
                return; // Skip spawning this specific shield to avoid overlapping bugs
            }
        }

        // Spawn the shield prefab into the scene
        Instantiate(shieldPrefab, spawnPos, Quaternion.identity);
    }
}