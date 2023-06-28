using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int fearLevel = 0;

    public Slider fearBarSlider;

    Vector3 moveDelta;
    public int keys_collected = 0;
    //public Rigidbody2D rb;
    public Animator animator;

    public float playerSpeed = 4.0f;

    private RaycastHit2D hit;

    private BoxCollider2D boxCollider;

    public AudioClip walkingSound;

    private AudioSource audioSource;

    public bool isWalking;



    private void Start()
    {   
        fearLevel = 60;
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        var er = GameObject.Find("ECGReceiver");
        if(er)
        {
            var comp = er.GetComponent<ECGReceiver>();
            comp.receivedHR.AddListener((hr) => { fearLevel = hr - 50; Debug.Log("hr: "+ hr); });
        }
    }




    private void FixedUpdate()
    {
        fearBarSlider.value = fearLevel;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //if (x > 0 || y > 0)
          //  Debug.Log(x + ", " + y);

        x *= playerSpeed;
        y *= playerSpeed;
        
        //if (x > 0 || y > 0)
          // Debug.Log(x + ", " + y);



         // Check if the player is walking
        bool wasWalking = isWalking; // Store the previous state of isWalking
        isWalking = (x != 0 || y != 0);
        animator.SetFloat("HorizontalSpeed", Mathf.Abs(x));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(y));
        
        moveDelta = new Vector3(x, y, 0);

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


        // Check if the walking state has changed
        if (isWalking && !wasWalking && !audioSource.isPlaying)
        {
        // Player started walking
            audioSource.PlayOneShot(walkingSound);
        }
        else if (!isWalking && wasWalking)
        {
        // Player stopped walking
            audioSource.Stop();
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

        // game over condition
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("He got you!");
            SceneManager.LoadScene("GameOverMenu");
        }
    }
}
