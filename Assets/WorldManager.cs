using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    [Header("Targets")]
    public Transform player;

    [Header("UI Panels")]
    public GameObject gameOverUI;
    public GameObject gameOverOverlay;

    [Header("Crash Effects")]
    public AudioClip crashSound;          // Drag your explosion/crash .mp3 here
    public GameObject crashVfxPrefab;    // Drag your smoke explosion prefab here

    private bool isGameOver = false;
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    public float GetSpawnZ(float forwardOffset)
    {
        if (player == null) return forwardOffset;
        return player.position.z + forwardOffset;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // NEW: Trigger satisfaction crunch sound and crash particles before freezing time
        PlayCrashEffects();

        Time.timeScale = 0.3f; // Dramatic slow-motion onset
        StartCoroutine(GameOverSequence());
    }

    void PlayCrashEffects()
    {
        if (audioSource != null && crashSound != null)
        {
            audioSource.PlayOneShot(crashSound, 0.9f);
        }

        if (crashVfxPrefab != null && player != null)
        {
            // Spawn explosion slightly in front of the car's current chassis coordinate
            Vector3 spawnPos = player.position + Vector3.forward * 1f; 
            GameObject vfx = Instantiate(crashVfxPrefab, spawnPos, Quaternion.identity);
            Destroy(vfx, 2f);
        }
    }

    IEnumerator GameOverSequence()
    {
        // Use WaitForSecondsRealtime because normal timeScale is slowed down!
        yield return new WaitForSecondsRealtime(0.25f);

        if (gameOverOverlay != null)
            gameOverOverlay.SetActive(true);

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            gameOverUI.transform.localScale = Vector3.zero;

            float t = 0f;
            float duration = 0.25f;

            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float scale = Mathf.Lerp(0f, 1f, t / duration);
                gameOverUI.transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }

            gameOverUI.transform.localScale = Vector3.one;
        }

        Time.timeScale = 0f; // Freeze game actions completely
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}