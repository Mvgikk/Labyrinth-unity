using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightTextController : MonoBehaviour
{
    public TextDisplayController textDisplay;
    public LayerMask interactableLayer;

    public float detectionRadius = 5f; 

    private bool hasSeenAll;
    private Dictionary<string, bool> hasSeenObjectDictionary = new Dictionary<string, bool>();
    
    void Start()
    {

        hasSeenObjectDictionary["Chest"] = false;
        hasSeenObjectDictionary["Floor_Goo"] = false;
        hasSeenObjectDictionary["Floor_Spike"] = false;
        hasSeenObjectDictionary["LargeLightPotion"] = false;
        hasSeenObjectDictionary["HidingBox"] = false;
        hasSeenObjectDictionary["Scroll"] = false;
    }


    void Update()
    {

        // Get all colliders within the detection radius of the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, interactableLayer);

        if(!hasSeenAll)
        {
            foreach (Collider2D collider in colliders)
            {
                string objectTag = collider.tag;
                CheckForObject(objectTag);
            }
            //Debug.Log("Sprawdza");
            if(CheckIfHasSeenAll()) hasSeenAll = true;
        }

    }



    

        void CheckForObject(string objectTag)
    {
        if (!hasSeenObjectDictionary.ContainsKey(objectTag))
        {
            return; // Ignore objects not in the dictionary
        }

        if (!hasSeenObjectDictionary[objectTag])
        {
            switch (objectTag)
            {
                case "Chest":
                    Debug.Log("widzi skrzynie");
                    textDisplay.UpdateText("I wonder what is inside. \n (E to interact)");
                    break;
                case "Floor_Goo":
                    Debug.Log("widzi sluz");
                    textDisplay.UpdateText("This place looks nasty");
                    break;
                case "Floor_Spike":
                    Debug.Log("widzi kolce");
                    textDisplay.UpdateText("I better avoid these..");
                    break;
                case "LargeLightPotion":
                    Debug.Log("widzi pota");
                    textDisplay.UpdateText("Oh, a Light Potion.");
                    break;
                case "HidingBox":
                    Debug.Log("widzi skrzynke");
                    textDisplay.UpdateText("Maybe i could hide in this box \n (E to interact)");
                    break;
                case "Scroll":
                    Debug.Log("widzi scroll");
                    textDisplay.UpdateText("This might help me escape.. \n I hope so");
                    break;
                default:
                    Debug.Log("default case");
                    break;    
 
            }

            hasSeenObjectDictionary[objectTag] = true;
        }
    }

    public bool CheckIfHasSeenAll()
    {
        foreach (bool status in hasSeenObjectDictionary.Values)
        {
            if (!status)
            {
                return false; 
            }
        }

        return true;
    }

}
    
