using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Data")]
    public int coins = 0;

    [Header("UI Reference")]
    public TMP_Text coinText;

    [Header("Juice Effects")]
    public AudioClip coinSound;          // Drag your coin pickup .mp3/.wav here
    public GameObject coinVfxPrefab;    // Drag your coin particle prefab here

    private AudioSource audioSource;

    void Awake()
    {
        instance = this;

        // Automatically add an AudioSource component if one doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Optimize speaker settings for fast, overlapping sound effects
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D Sound (equally loud everywhere)
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public void PlayCoinEffects(Vector3 spawnPosition)
    {
        // 1. Play satisfaction sound clip
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound, 0.7f); // 0.7f is the volume scale
        }

        // 2. Spawn visual particles at the coin's exact world position
        if (coinVfxPrefab != null)
        {
            GameObject vfx = Instantiate(coinVfxPrefab, spawnPosition, Quaternion.identity);
            Destroy(vfx, 1.5f); // Automatically clear memory after particles finish bursting
        }
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins;
        }
    }
}