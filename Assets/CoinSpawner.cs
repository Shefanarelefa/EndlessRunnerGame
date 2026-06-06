using UnityEngine;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;

    public float spawnDistance = 30f;
    public float spacingZ = 8f;

    public int minCoins = 2;
    public int maxCoins = 4;

    private float[] lanes = new float[] { -4f, -2f, 0f, 2f, 4f };

    private float nextZ;

    void Start()
    {
        nextZ = Camera.main.transform.position.z + spawnDistance;
    }

    void Update()
    {
        float camZ = Camera.main.transform.position.z;

        if (camZ + spawnDistance > nextZ)
        {
            SpawnCoinRow(nextZ);
            nextZ += spacingZ;
        }
    }

    void SpawnCoinRow(float z)
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1);

        List<int> safeLanes = GetSafeLanes(z);

        for (int i = 0; i < coinCount; i++)
        {
            if (safeLanes.Count == 0)
                return;

            int laneIndex = Random.Range(0, safeLanes.Count);
            int lane = safeLanes[laneIndex];

            float x = lanes[lane];

            Vector3 pos = new Vector3(x, 0.5f, z);
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }
    }

    List<int> GetSafeLanes(float z)
    {
        List<int> safe = new List<int>();

        for (int i = 0; i < lanes.Length; i++)
        {
            if (!IsLaneBlocked(lanes[i], z))
            {
                safe.Add(i);
            }
        }

        return safe;
    }

    bool IsLaneBlocked(float x, float z)
    {
        // simple overlap check
        Collider[] hits = Physics.OverlapSphere(new Vector3(x, 0.5f, z), 0.5f);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Obstacle"))
                return true;
        }

        return false;
    }
}