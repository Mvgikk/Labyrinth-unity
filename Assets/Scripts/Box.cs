using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject player;
    public bool hasInteracted = false; //bool to check if player has already interacted with chest
    public void Interact()
    {
        player.SetActive(false);
    }
}
