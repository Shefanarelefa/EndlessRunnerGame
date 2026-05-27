using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    public Transform player;

    public GameObject gameOverUI;
    public GameObject gameOverOverlay;

    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    public float GetSpawnZ(float forwardOffset)
    {
        return player.position.z + forwardOffset;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        Time.timeScale = 0.3f;

        StartCoroutine(GameOverSequence());
    }

    System.Collections.IEnumerator GameOverSequence()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        // 🔴 Red screen overlay
        if (gameOverOverlay != null)
            gameOverOverlay.SetActive(true);

        // 🧠 UI pop-in
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

        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}