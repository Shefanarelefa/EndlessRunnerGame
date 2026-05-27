using UnityEngine;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    public static DistanceManager instance;

    public Transform player;
    public TMP_Text distanceText;

    private float startZ;
    public float distance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        startZ = player.position.z;
    }

    void Update()
    {
        distance = player.position.z - startZ;

        if (distanceText != null)
            distanceText.text = "Distance: " + Mathf.FloorToInt(distance);
    }
}