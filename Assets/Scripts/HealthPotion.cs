using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;


public class HealthPotion : MonoBehaviour
{
    public AudioClip drinkSound; // Sound effect to play when drinking the potion

   // public GameObject effectPrefab; // Particle effect to play when drinking the potion
    private bool hasDisplayedText = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthController player = collision.GetComponent<PlayerHealthController>();
            if (player != null)
            {
                player.ReplenishHealth();      
                PlayDrinkSound();
                Destroy(gameObject); // Destroy the potion after it has been consumed
            }
        }
    }

    private void PlayDrinkSound()
    {
        AudioSource.PlayClipAtPoint(drinkSound, transform.position);
    }
}
