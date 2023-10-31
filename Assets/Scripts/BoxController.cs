using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public HidingBox currentBox { set; get; }
    public SoundManager soundManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentBox != null)
        {
            soundManager.PlayBoxEnterEffect();
            currentBox.Interact();
        }
    }
}
