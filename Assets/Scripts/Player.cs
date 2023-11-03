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

    public Rigidbody2D rb;
    public Animator animator;

    public float playerSpeed = 4.0f;

    private RaycastHit2D hit;

    private BoxCollider2D boxCollider;

    public AudioClip walkingSound;

    public AudioClip gooWalkingSound;

    public MapController mapController;

    private AudioSource audioSource;

    public SoundManager soundManager;


    public bool logHr = true;
    public bool isWalking;
    public bool isDead = false;

    public bool isHidden = false;
    public bool isInGoo = false;

    private void Start()
    {
        fearLevel = 60;
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        var er = GameObject.Find("ECGReceiver");
        if(er)
        {
            Debug.Log("mam ECG receiver");
            var comp = er.GetComponent<ECGReceiver>();
            comp.receivedHR.AddListener((hr) => { 
                fearLevel = hr - 50;
                if (logHr) Debug.Log("hr: "+ hr);
            });
        }
        else
        {
            Debug.Log("NIE ZNALAZ£EM");
        }
    }

    private void FixedUpdate()
    {
        fearBarSlider.value = fearLevel;
        if (!mapController.isMapVisible){
            if (!isHidden)
            {
                
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");

                x *= playerSpeed;
                y *= playerSpeed;
    

                // Check if the player is walking
                bool wasWalking = isWalking; // Store the previous state of isWalking
                isWalking = (x != 0 || y != 0);
                animator.SetFloat("HorizontalSpeed", Mathf.Abs(x));
                animator.SetFloat("VerticalSpeed", Mathf.Abs(y));
                if (!isDead)
                {
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
                }

                if (isWalking)
                {
                    if (isInGoo)
                    {
                        if (!audioSource.isPlaying || audioSource.clip != gooWalkingSound)
                        {
                            // Player started walking in goo
                            audioSource.clip = gooWalkingSound;
                            audioSource.Play();
                        }
                    }
                    else
                    {
                        if (!audioSource.isPlaying || audioSource.clip != walkingSound)
                        {
                            // Player started walking
                            audioSource.clip = walkingSound;
                            audioSource.Play();
                        }
                    }
                }
                else if (wasWalking)
                {
                    // Player stopped walking
                    audioSource.Stop();
                }
            }
        }
    }

    public void HidePlayer()
    {
        this.isHidden = true;
        transform.localScale = new Vector3(0, 0, 0);
        audioSource.Stop();
    }

    public void ShowPlayer()
    {
        this.isHidden = false;
        transform.localScale = new Vector3(1, 1, 1);
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {


        // game over condition
        if (other.gameObject.tag == "Monster" && isDead == false && isHidden == false)
        {
            Die();
        }
    }

    public void Die()
    {
        //nwm nie dziala jak sie trzyma guzik
        if(!isDead){
        soundManager.PlayDeathSound();
        isWalking=false;
        isDead = true;
        animator.SetBool("isDead", true);
        //animator.enabled=false;
        rb.bodyType= RigidbodyType2D.Static;
        audioSource.Stop();
        //ShowGameOverMenu();
        DelayedRestart(0.5f);
        


        }

    }

    public void ShowGameOverMenu()
    {
        var ecgReceiver = GameObject.Find("ECGReceiver");
        if (ecgReceiver != null)
            Destroy(ecgReceiver);
        SceneManager.LoadScene("GameOverMenu");
    }
    IEnumerator DelayedRestart(float delay)
{
    yield return new WaitForSeconds(delay);
    //animator.enabled=false;
    ShowGameOverMenu();

}
}
