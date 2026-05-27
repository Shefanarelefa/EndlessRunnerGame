using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public float rotateSpeed = 180f;

    private bool collected = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Continuous spin
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Add coin to manager
            CoinManager.instance.AddCoin(coinValue);

            // Visual feedback (pop effect)
            StartCoroutine(CollectEffect());
        }
    }

    System.Collections.IEnumerator CollectEffect()
    {
        // instantly stop interaction
        GetComponent<Collider>().enabled = false;

        // shrink effect (feel feedback)
        float t = 0f;
        float duration = 0.1f;

        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}