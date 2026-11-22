
using UnityEngine;
using TMPro;

// Counts the coins. It listens to Coin.Collected:
// no coin needs to know that this object exists.
public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int coins; // coins collected so far
    private int coinsToWin; // coins in the scene at the start

    void OnEnable()
    {
        Coin.Collected += AddCoin; // subscribe
    }

    void OnDisable()
    {
        Coin.Collected -= AddCoin; // always unsubscribe
    }

    void Start()
    {
        coinsToWin = FindObjectsByType<Coin>().Length;

        UpdateScore();

        Debug.Log($"Collect the {coinsToWin} coins!");
    }

    private void AddCoin()
    {
        coins++;

        UpdateScore();

        Debug.Log($"Coins: {coins} / {coinsToWin}");

        if (coins == coinsToWin)
        {
            Debug.Log("All coins collected!");
        }
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Coins: {coins} / {coinsToWin}";
        }
    }
}
