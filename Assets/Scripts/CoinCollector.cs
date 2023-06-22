using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int coinCount = 0; // Counter for collected coins
    public SoundManager soundManager;



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

        soundManager.PlayCoinCollectEffect();
        // Disable the coin object
        Destroy(coin);
    }
}