using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    public Animator animator;
    public float activationDistance = 1.5f; // The maximum distance at which the player can activate the lever
    private bool isFull = false;

    private bool hasInteracted = false; //bool to check if player has already interacted with chest
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerInRange())
            {
                InteractWithChest();
            }
        }
    }
    public void Interact()
    {
        if (hasInteracted)
        {
            Debug.Log("Player already interacted with the chest!");
            return;
        }
        if (isFull)
        {
            Debug.Log("You found treasure in the chest!");
            
        }
        else
        {
            Debug.Log("The chest is empty!");
        }

        animator.SetBool("isFull", isFull);
        animator.SetTrigger("Interact");
        hasInteracted = true;
    }

    public void SetFull(bool full)
    {
        isFull = full;
        animator.SetBool("isFull", isFull);
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

    public void InteractWithChest()
{
    // Find the chest object or obtain a reference to it using other means
    ChestController chest = FindObjectOfType<ChestController>();

    // Randomly determine if the chest is empty or full
    bool isFull = Random.value < 0.5f;

    // Set the chest state and trigger the animation
    chest.SetFull(isFull);
    chest.Interact();
}


}
