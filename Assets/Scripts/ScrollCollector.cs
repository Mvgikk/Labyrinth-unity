using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollCollector : MonoBehaviour
{

    public int totalScrolls;       // Total number of scrolls in the scene
    public int totalGoos;
    private int scrollCount = 0; // Counter for collected scrolls
    public ScrollCounter scrollCounter; // Reference to the scrollCounter component
    public SoundManager soundManager;

    public MapController mapController;

    public TextDisplayController textDisplay;

    public GameObject mapText;

    public GameObject floorEscape;

    public GameObject[] gooObjects;

    private GameObject randomGoo;

    public GameObject escapeArrow;
    public GameObject arrowParticleSystem;





    private void Start()
    {
        // Get the initial total number of scrolls in the scene
        GameObject[] scrollObjects = GameObject.FindGameObjectsWithTag("Scroll");
        gooObjects = GameObject.FindGameObjectsWithTag("Floor_Goo");

        totalScrolls = scrollObjects.Length;
        totalGoos = gooObjects.Length;
       // Debug.Log(totalGoos);
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
            
            // zmien losowego floor goo na wyjscie
            floorEscape.SetActive(true);
            //strzalka
            escapeArrow.SetActive(true);
            //particles
            arrowParticleSystem.SetActive(true);

            

            int randomIndex = Random.Range(0,totalGoos);
            Debug.Log("Random index " + randomIndex );

            randomGoo = gooObjects[randomIndex];
            SpriteRenderer randomGooSpriteRenderer = randomGoo.GetComponent<SpriteRenderer>();
            //Floor_Goo randomGooScript = randomGoo.GetComponent<Floor_Goo>();
            randomGooSpriteRenderer.enabled = false;
            //randomGooScript.enabled = false;
            
            Vector3 randomGooPosition = randomGoo.transform.position;
            floorEscape.transform.position = randomGooPosition;
            randomGooPosition.y += 1;
            escapeArrow.transform.position = randomGooPosition;
            arrowParticleSystem.transform.position = randomGooPosition;
            



            mapController.hasCollectedAllScrolls = true;
            textDisplay.UpdateText("I have collected all map pieces..");
            mapText.SetActive(true);
            Debug.Log("got all scrolls!");
            randomGoo.SetActive(false);
            //SceneManager.LoadScene("WinMenu");

        }


        // Disable the scroll object
        //Destroy(scroll);
        Destroy(scroll.transform.parent.gameObject);
    }
}