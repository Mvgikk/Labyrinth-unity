using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;


public class LargeLightPotion : MonoBehaviour
{
    public float intensityIncreaseAmount = 1f; // Amount to increase the light intensity
    public float intensityRadius = 0.05f; // Amount to increase the light radius
    public AudioClip drinkSound; // Sound effect to play when drinking the potion


    public TextDisplayController textDisplay;

   // public GameObject effectPrefab; // Particle effect to play when drinking the potion
    private bool hasDisplayedText = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Light2D playerLight = collision.GetComponent<Light2D>();
            if (playerLight != null)
            {
                playerLight.intensity += intensityIncreaseAmount;             
                playerLight.pointLightOuterRadius += intensityIncreaseAmount;             
                PlayDrinkSound();
                if(!hasDisplayedText)
                {
                textDisplay.UpdateText("Potions increase player's light intesity");
                hasDisplayedText = true; 
                }
               // PlayEffect();
                Destroy(gameObject); // Destroy the potion after it has been consumed
            }
        }
    }

    private void PlayDrinkSound()
    {
        AudioSource.PlayClipAtPoint(drinkSound, transform.position);
    }
    

   /* private void PlayEffect()
    {
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
    }
    */
}
