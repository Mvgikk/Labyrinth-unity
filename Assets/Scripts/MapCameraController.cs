using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour
{   
    public MapController mapController;
    public float moveSpeed = 35.0f;

    void FixedUpdate()
    {
        if (mapController.isMapVisible)
        {

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");


            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);


            moveDirection.Normalize();

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

    }
}
