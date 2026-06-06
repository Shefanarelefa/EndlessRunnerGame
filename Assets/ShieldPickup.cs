using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        CarMovement car = other.GetComponent<CarMovement>();

        if (car != null)
        {
            car.ActivateShield();
        }

        Debug.Log("🛡 SHIELD COLLECTED");

        Destroy(gameObject);
    }
}