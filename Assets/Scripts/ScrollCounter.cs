using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollCounter : MonoBehaviour
{
    private TextMeshProUGUI scrollText;
    public ScrollCollector scrollCollector;


    private void Start()
    {
        scrollText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScrollCount(int count)
    {
        scrollText.text = "Scrolls: " + count + " / " + scrollCollector.totalScrolls;
    }
}