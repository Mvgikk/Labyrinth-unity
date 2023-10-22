using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

 private Chest currentChest;
 public SoundManager soundManager;
 

public TextDisplayController textDisplay;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentChest != null && !currentChest.hasInteracted)
        {
            currentChest.InteractWithChest();
            soundManager.PlayChestOpenEffect();
        }
    }

        private void OnCollisionEnter2D(Collision2D collision) 
    {
        Chest chest = collision.collider.GetComponent<Chest>();
        if (chest != null)
        {
            currentChest = chest;
        }
    }


}
