using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollCollector : MonoBehaviour
{

    public int totalScrolls;       // Total number of scrolls in the scene
    private int scrollCount = 0; // Counter for collected scrolls
    public ScrollCounter scrollCounter; // Reference to the scrollCounter component
    public SoundManager soundManager;




    private void Start()
    {
        // Get the initial total number of scrolls in the scene
        GameObject[] scrollObjects = GameObject.FindGameObjectsWithTag("Scroll");

        totalScrolls = scrollObjects.Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scroll"))
        {
            CollectScroll(collision.gameObject);
        }
    }
    private void CollectScroll(GameObject scroll)
    {
        scrollCount++; // Increment the scroll count

        
        Debug.Log("scroll collected! Total scrolls: " + scrollCount);
        Debug.Log("total scrolls : " + totalScrolls);
        scrollCounter.UpdateScrollCount(scrollCount); // Update the displayed scroll count
        soundManager.PlayScrollCollectEffect();
        if (scrollCount >= totalScrolls)
        {
            // Player has collected all scrolls, trigger win condition
            Debug.Log("You win!");
            SceneManager.LoadScene("WinMenu");

        }


        // Disable the scroll object
        //Destroy(scroll);
        Destroy(scroll.transform.parent.gameObject);
    }
}