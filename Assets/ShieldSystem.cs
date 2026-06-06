using UnityEngine;

public class ShieldSystem : MonoBehaviour
{
    public bool hasShield = false;

    public GameObject shieldVisual; // optional (можеш да сложиш blue glow)

    public void GiveShield()
    {
        hasShield = true;

        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        Debug.Log("🛡 SHIELD GAINED");
    }

    public bool UseShield()
    {
        if (!hasShield)
            return false;

        hasShield = false;

        if (shieldVisual != null)
            shieldVisual.SetActive(false);

        Debug.Log("💥 SHIELD USED");
        return true;
    }
}