using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins = 0;

    public TMP_Text coinText;

    void Awake()
    {
        instance = this;
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText == null)
        {
            Debug.LogError("coinText is NOT assigned!");
            return;
        }

        coinText.text = "Coins: " + coins;
    }
}