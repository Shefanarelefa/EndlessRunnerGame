using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Simplified to allow CarMovement handles to manage collision parsing cleanly
    private void Start()
    {
        if (!gameObject.CompareTag("Obstacle"))
        {
            gameObject.tag = "Obstacle";
        }
    }
}