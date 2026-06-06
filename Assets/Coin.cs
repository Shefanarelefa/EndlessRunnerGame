using UnityEngine;
using System.Collections;

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
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;

            // Send coin data to counter
            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoin(coinValue);
                
                // NEW: Tell manager to play sound and spawn sparks right here!
                CoinManager.instance.PlayCoinEffects(transform.position);
            }

            StartCoroutine(CollectEffect());
        }
    }

    IEnumerator CollectEffect()
    {
        GetComponent<Collider>().enabled = false;

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