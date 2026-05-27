using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float speedIncrease = 0.5f;
    public float maxSpeed = 25f;

    public float laneDistance = 2f;

    private int currentLane = 2;

    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    public GameObject restartText;

    private int score = 0;
    private float scoreTimer = 0f;

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        // lane input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane++;

        currentLane = Mathf.Clamp(currentLane, 0, 4);

        // SCORE
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= 1f)
        {
            score++;
            scoreTimer = 0f;

            if (scoreText != null)
                scoreText.text = "Score: " + score;
        }

        // SPEED INCREASE (NEW)
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += speedIncrease * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        // forward movement
        transform.position += Vector3.forward * forwardSpeed * Time.fixedDeltaTime;

        // lane movement
        float targetX = (currentLane - 2) * laneDistance;

        Vector3 pos = transform.position;
        pos.x = targetX;

        transform.position = Vector3.Lerp(transform.position, pos, 0.25f);
    }

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Obstacle"))
    {
        if (isGameOver) return;

        isGameOver = true;

        forwardSpeed = 0f;

        ObstacleSpawner spawner = FindObjectOfType<ObstacleSpawner>();
        if (spawner != null)
            spawner.StopSpawning();

        StartCoroutine(GameOverRoutine());
    }
}

System.Collections.IEnumerator GameOverRoutine()
{
    yield return new WaitForSeconds(0.4f);

    if (gameOverText != null)
        gameOverText.SetActive(true);

    if (restartText != null)
        restartText.SetActive(true);
}

void ShowGameOverUI()
{
    if (gameOverText != null)
        gameOverText.SetActive(true);

    if (restartText != null)
        restartText.SetActive(true);
}
}