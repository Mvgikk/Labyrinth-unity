using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Box currentBox;
    public SoundManager soundManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentBox != null)
        {
            soundManager.PlayBoxEnterEffect();
            currentBox.Interact();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Box box = collision.collider.GetComponent<Box>();
        if (box != null)
        {
            currentBox = box;
        }
    }
}
