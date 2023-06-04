using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
 
    Vector3 moveDelta;
    public int keys_collected = 0;
    //public Rigidbody2D rb;
    public Animator animator;


    private RaycastHit2D hit;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        //float moveAmountVer = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        //float moveAmountHor = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //transform.Translate(0,moveAmountVer,0);
        //transform.Translate(moveAmountHor, 0, 0);


    }

    private void FixedUpdate()
    {


        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("HorizontalSpeed", Mathf.Abs(x));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(y));



        
        moveDelta = new Vector3(x, y, 0);

        Debug.Log(x);
        Debug.Log(y);

        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        //check if can move in the vertical director by casting a collider box there. If null -> can move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Walls"));

        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Walls"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
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