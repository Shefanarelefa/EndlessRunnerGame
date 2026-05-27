using UnityEngine;

public class LaneFollower : MonoBehaviour
{
    public Transform player;
    public float resetZ = -10f;
    public float forwardZ = 40f;

    void Update()
    {
        if (player.position.z - transform.position.z > 30f)
        {
            Vector3 pos = transform.position;
            pos.z += forwardZ;
            transform.position = pos;
        }
    }
}