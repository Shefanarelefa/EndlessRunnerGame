using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (WorldManager.instance != null)
            {
                WorldManager.instance.GameOver();
            }
        }
    }
}