using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LargeInvisPotion : MonoBehaviour
{
    public AudioClip drinkSound; // Sound effect to play when drinking the potion

   // public GameObject effectPrefab; // Particle effect to play when drinking the potion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject monster = GameObject.Find("Monster");

            DemonController demonController = monster.GetComponent<DemonController>();

            //demonController.debuff();

            PlayDrinkSound();

            Destroy(gameObject); // Destroy the potion after it has been consumed
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
