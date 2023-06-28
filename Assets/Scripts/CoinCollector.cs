using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{

    public int totalCoins;       // Total number of coins in the scene
    private int coinCount = 0; // Counter for collected coins
    public CoinCounter coinCounter; // Reference to the CoinCounter component
    public SoundManager soundManager;




    private void Start()
    {
        // Get the initial total number of coins in the scene
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("Coin");

        totalCoins = coinObjects.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject);
        }
    }
    private void CollectCoin(GameObject coin)
    {
        coinCount++; // Increment the coin count

        
        Debug.Log("Coin collected! Total coins: " + coinCount);
        Debug.Log("total coins : " + totalCoins);
        coinCounter.UpdateCoinCount(coinCount); // Update the displayed coin count
        soundManager.PlayCoinCollectEffect();
        if (coinCount >= totalCoins)
        {
            // Player has collected all coins, trigger win condition
            Debug.Log("You win!");
            SceneManager.LoadScene("WinMenu");

        }


        // Disable the coin object
        Destroy(coin);
    }
}