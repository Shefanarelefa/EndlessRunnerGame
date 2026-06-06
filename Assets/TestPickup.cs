using UnityEngine;

public class TestPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("🔥 TRIGGER WORKS WITH: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ PLAYER HIT PICKUP");
            Destroy(gameObject);
        }
    }
}