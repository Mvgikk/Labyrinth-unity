using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Goo : MonoBehaviour
{
    public float slowdownSpeed = 0.5f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {

                player.isInGoo = true;
                Debug.Log("Chlop jest w mazi");
                player.playerSpeed = slowdownSpeed;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.isInGoo = false;
                Debug.Log("Chlop wyszedl z mazi");
                player.playerSpeed = player.maxSpeed;
            }
        }
    }

}
