using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 10f, -12f);

    public float followSpeed = 8f;

    public float tiltAngle = 35f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        // Slight angle from behind (key for 2D + depth feel)
        transform.rotation = Quaternion.Euler(tiltAngle, 0f, 0f);
    }
}