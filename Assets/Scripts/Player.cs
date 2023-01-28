using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public int keys_collected = 0;



    void Update()
    {
        float moveAmountVer = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveAmountHor = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(0,moveAmountVer,0);
        transform.Translate(moveAmountHor, 0, 0);


    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Walls")
        {
            //TODO utrata hp

            Debug.Log("Wall hit");
        } 
        if(other.gameObject.tag == "Keys")
        {
            keys_collected += 1;
            Destroy(other.gameObject);
        }  
        if(other.gameObject.tag == "Exit" && keys_collected == 3)
        {
            Debug.Log("Win");
        }
    }
}
