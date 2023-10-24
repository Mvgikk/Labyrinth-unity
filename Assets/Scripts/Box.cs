using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool playerIsHiding = false;
    public GameObject player;
    public SoundManager soundManager;
    public void Interact()
    {
        if (!playerIsHiding)
        {
            player.GetComponent<Player>().HidePlayer();
            playerIsHiding = true;
        }
        else
        {
            player.GetComponent<Player>().ShowPlayer();
            playerIsHiding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.GetComponent<BoxController>().currentBox = this;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player.GetComponent<BoxController>().currentBox = null;
    }
}
