using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 10f;
    public float maxSpeed = 25f;
    public float speedIncrease = 0.5f;
    public float laneDistance = 2f;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    public GameObject restartText;

    private int currentLane = 2;
    private int score = 0;
    private float scoreTimer = 0f;
    private bool isGameOver = false;

    [Header("PowerUps")]
    public bool hasShield = false;

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        HandleInput();
        HandleForward();
        HandleScore();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane++;

        currentLane = Mathf.Clamp(currentLane, 0, 4);

        float targetX = (currentLane - 2) * laneDistance;

        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(transform.position.x, targetX, 15f * Time.deltaTime);
        transform.position = pos;
    }

    void HandleForward()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;

        if (forwardSpeed < maxSpeed)
            forwardSpeed += speedIncrease * Time.deltaTime;
    }

    void HandleScore()
    {
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= 1f)
        {
            score++;
            scoreTimer = 0f;

            if (scoreText != null)
                scoreText.text = "Score: " + score;
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
        Debug.Log("🛡️ SHIELD ON");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (hasShield)
            {
                hasShield = false;
                Destroy(collision.gameObject);
                Debug.Log("🛡️ SHIELD CONSUMED!");
                return;
            }

            TriggerGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (other.CompareTag("Obstacle"))
        {
            if (hasShield)
            {
                hasShield = false;
                Destroy(other.gameObject);
                Debug.Log("🛡️ SHIELD CONSUMED!");
                return;
            }

            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        forwardSpeed = 0f;
        Debug.Log("💀 GAME OVER CALLED");

        if (WorldManager.instance != null)
        {
            WorldManager.instance.GameOver();
        }
        else
        {
            if (gameOverText != null) gameOverText.SetActive(true);
            if (restartText != null) restartText.SetActive(true);
        }
    }
}