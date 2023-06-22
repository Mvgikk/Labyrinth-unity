using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lever : MonoBehaviour
{
    public bool isActivated = false;
    public GameObject targetObject; // The object to activate or deactivate when the lever is pulled
    public float activationDistance = 1.5f; // The maximum distance at which the player can activate the lever
    public AudioClip pullSound; // Sound effect to play when the lever is pulled
    public AudioSource leverAudioSource; // Reference to the AudioSource component

    public Sprite pulledSprite; // The sprite to use when the lever is pulled
    private SpriteRenderer leverSpriteRenderer; // Reference to the Sprite Renderer component
    private Sprite initialSprite; // Reference to the lever's initial sprite





    private void Start()
    {
        leverSpriteRenderer = GetComponent<SpriteRenderer>();
        initialSprite = leverSpriteRenderer.sprite;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerInRange())
            {
                PullLever();
            }
        }
    }

    private bool IsPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= activationDistance;
        }

        return false;
    }

    private void PullLever()
    {
        isActivated = !isActivated; 

        // Perform the desired action based on the lever state
        if (isActivated)
        {
            Debug.Log("Lever activated!");
            // Activate or enable the target object, e.g., targetObject.SetActive(true);
            leverSpriteRenderer.sprite = pulledSprite; // Change the lever sprite
            leverAudioSource.PlayOneShot(pullSound); // Play the lever pull sound effect
            
        }
        else
        {
            Debug.Log("Lever deactivated!");
            // Deactivate or disable the target object, e.g., targetObject.SetActive(false);
            leverSpriteRenderer.sprite = initialSprite; // Revert the lever sprite back to the initial state
            leverAudioSource.PlayOneShot(pullSound); // Play the lever pull sound effect
        }
    }
}