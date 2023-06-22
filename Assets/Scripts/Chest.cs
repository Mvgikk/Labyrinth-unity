using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public Animator animator;
    public float interactionDistance = 2f;

    private bool isFull = false;

    public bool hasInteracted = false; //bool to check if player has already interacted with chest
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

    public void InteractWithChest()
{
    // Randomly determine if the chest is empty or full
    bool isFull = Random.value < 0.5f;

    // Set the chest state and trigger the animation
    this.SetFull(isFull);
    this.Interact();
    isFull = Random.value < 0.5f;
}


}
