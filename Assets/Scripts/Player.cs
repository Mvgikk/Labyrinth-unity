using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int fearLevel = 0;
    public int maxHealth = 100;
    public int currentHealth;
    public float invulnerabilityDuration = 2f; // Adjust the duration as needed
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;

    public Slider fearBarSlider;

    public Slider healthBarSlider;

    Vector3 moveDelta;

    public Rigidbody2D rb;

    public Animator animator;

    public float playerSpeed;
    public float maxSpeed = 4.5f;

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
        currentHealth = maxHealth;
        playerSpeed = maxSpeed;
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
        healthBarSlider.value = currentHealth;

        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            // Check if invulnerability has ended
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }

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

    public void TakeDamage()
    {
        if (!isInvulnerable)
        {
            currentHealth -= 20;
            playerSpeed -= 0.5f;
            if (currentHealth < 0)
            {
                Die();
            }
            // Apply invulnerability
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
        }
    }

    public void ReplenishHealth()
    {
        currentHealth = maxHealth;
        playerSpeed = maxSpeed;
    }

    public void Die()
    {
        if(!isDead){
            soundManager.PlayDeathSound();
            isWalking=false;
            isDead = true;
            animator.SetBool("isDead", true);
            rb.bodyType= RigidbodyType2D.Static;
            audioSource.Stop();
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
        ShowGameOverMenu();
    }
}
