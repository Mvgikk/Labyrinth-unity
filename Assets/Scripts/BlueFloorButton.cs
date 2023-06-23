using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFloorButton : MonoBehaviour
{
    private bool isClicked = false;
    private bool hasInteracted = false;
    private Animator animator;

    public GameObject targetObject; // The object to activate or deactivate when the button is clicked

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasInteracted)
        {
            // Player stepped on the button
            isClicked = true;
            Debug.Log("Button Clicked!");
            animator.SetTrigger("IsClicked");
            hasInteracted = true;
        }
    }


}
