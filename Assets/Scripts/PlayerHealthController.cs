using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float invulnerabilityDuration = 2f; // Adjust the duration as needed
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;
    public Player player;
    public GameObject empty1;
    public GameObject empty2;
    public GameObject empty3;
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;

            // Check if invulnerability has ended
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }
    }

    public void FillHealthBar()
    {
        empty1.SetActive(false);
        empty2.SetActive(false);
        empty3.SetActive(false);
    }

    public void ReplenishHealth()
    {
        currentHealth = maxHealth;
        player.ResetSpeed();
        FillHealthBar();
    }

    public void TakeDamage()
    {
        if (!isInvulnerable)
        {
            currentHealth -= 1;
            player.LooseSpeed();
            switch(currentHealth)
            {
                case 2:
                    empty3.SetActive(true);
                    break;
                case 1:
                    empty2.SetActive(true);
                    break;
                case 0:
                    empty1.SetActive(true);
                    break;
            }
            if (currentHealth <= 0)
            {
                player.Die();
            }
            else
            {
                soundManager.PlayHurtEffect();
            }
            // Apply invulnerability
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
        }
    }
}
