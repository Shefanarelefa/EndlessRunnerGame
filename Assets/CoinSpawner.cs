using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Transform player;

    public float spawnAhead = 25f;
    public float spacingZ = 10f;

    private float[] lanes = new float[] { -4f, -2f, 0f, 2f, 4f };
    private float nextZ;

void Start()
{
    if (player == null)
    {
        Debug.LogError("PLAYER NOT ASSIGNED");
        return;
    }

    // IMPORTANT: start closer, not far ahead
    nextZ = player.position.z + 10f;

    Debug.Log("Coin system reset. nextZ = " + nextZ);
}

 void Update()
{
    if (player == null) return;

    float targetZ = player.position.z + spawnAhead;

    if (targetZ > nextZ)
    {
        SpawnRow(nextZ);
        nextZ += spacingZ;
    }
}

   void SpawnRow(float z)
{
    if (coinPrefab == null)
    {
        Debug.LogError("COIN PREFAB IS NOT ASSIGNED!");
        return;
    }

    int coins = Random.Range(0, 3);

    for (int i = 0; i < coins; i++)
    {
        int lane = Random.Range(0, lanes.Length);

        Vector3 pos = new Vector3(lanes[lane], 0.5f, z);
        Instantiate(coinPrefab, pos, Quaternion.identity);
    }
}
}