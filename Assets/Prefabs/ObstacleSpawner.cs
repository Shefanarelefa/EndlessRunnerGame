using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    public float spawnDistance = 30f;
    public float spacingZ = 10f;
    public int rowsAhead = 5;

    private float[] lanes = new float[] { -4f, -2f, 0f, 2f, 4f };

    private float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = Camera.main.transform.position.z + spawnDistance;

        for (int i = 0; i < rowsAhead; i++)
        {
            SpawnRow(nextSpawnZ + i * spacingZ);
        }
    }

    void Update()
    {
        float camZ = Camera.main.transform.position.z;

        if (camZ + spawnDistance > nextSpawnZ)
        {
            SpawnRow(nextSpawnZ);
            nextSpawnZ += spacingZ;
        }
    }

    void SpawnRow(float zPos)
    {
        int laneIndex = Random.Range(0, lanes.Length);

        Vector3 spawnPos = new Vector3(
            lanes[laneIndex],
            0.5f,
            zPos
        );

        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        enabled = false;
    }
}