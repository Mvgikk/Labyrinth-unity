using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Goo : MonoBehaviour
{
    public float playerDefaultSpeed = 4.0f;
    public float slowdownSpeed = 0.5f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {

                Debug.Log("W mazi jest");
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
                Debug.Log("Z mazi wyszed");
                player.playerSpeed = playerDefaultSpeed;
            }
        }
    }

}
