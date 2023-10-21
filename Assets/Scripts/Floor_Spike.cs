using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Floor_Spike : MonoBehaviour
{
    private bool spikesShown = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {

                Debug.Log("Kolce wszedl");
                //TODO gracz smierc
                if(spikesShown)
                {
                    player.Die();
                }
            }
        }
    }

    public void SpikesShow()
    {
        spikesShown = true;
    }

    public void SpikesHide()
    {
        spikesShown = false;
    }
}
