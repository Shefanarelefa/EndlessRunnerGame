using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    public Transform player;
    public GameObject roadPrefab;

    public float roadLength = 30f;
    public int numberOfSegments = 3;

    private float spawnZ = 0f;

    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (numberOfSegments * roadLength))
        {
            SpawnRoad();
            DeleteOldRoad();
        }
    }

    void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeRoads.Add(road);

        spawnZ += roadLength;
    }

    void DeleteOldRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}