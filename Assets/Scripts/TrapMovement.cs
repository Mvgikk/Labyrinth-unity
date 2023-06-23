using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapMovement : MonoBehaviour
{
    public float moveSpeedX = 4.0f; // Speed at which the trap moves on the X-axis
    public float moveDistanceX = 4.5f; // Distance the trap moves on the X-axis before looping back

    public float moveSpeedY = 4.0f; // Speed at which the trap moves on the Y-axis
    public float moveDistanceY = 4.5f; // Distance the trap moves on the Y-axis before looping back

    private float initialPositionX; // Initial X position of the trap
    private float initialPositionY; // Initial Y position of the trap

    public bool movesOnX = true;    

    private void Start()
    {
        initialPositionX = transform.position.x;
        initialPositionY = transform.position.y;
    }

    private void Update()
    {
        if(movesOnX)
        {
            MoveOnXAxis();
        }
        else
        {
            MoveOnYAxis();
        }

    }

    private void MoveOnXAxis()
    {
        // Calculate the new X position
        float newX = initialPositionX + Mathf.PingPong(Time.time * moveSpeedX, moveDistanceX);

        // Update the trap's position
        Vector3 newPosition = transform.position;
        newPosition.x = newX;
        transform.position = newPosition;
    }

    private void MoveOnYAxis()
    {
        // Calculate the new Y position
        float newY = initialPositionY + Mathf.PingPong(Time.time * moveSpeedY, moveDistanceY);

        // Update the trap's position
        Vector3 newPosition = transform.position;
        newPosition.y = newY;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {

                Debug.Log("Trap wszedl");
                //TODO gracz smierc

            }
        }
    }
}