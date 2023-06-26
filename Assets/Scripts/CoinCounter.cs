using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    private void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinCount(int count)
    {
    coinText.text = "Coins: " + count;
    }
}