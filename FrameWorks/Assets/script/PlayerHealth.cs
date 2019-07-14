using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;                            // The amount of health the player starts the game with.
    [SerializeField] private int currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    //public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
                                                                // PlayerMovement playerMovement;                              // Reference to the player's movement.
                                                                // PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    // Use this for initialization
    void Start()
    {
        // Set the initial health of the player.
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth;
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            //damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
       

        // Play the hurt sound effect.
        //playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        // playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Turn off the movement and shooting scripts.
        // playerMovement.enabled = false;
        //  playerShooting.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        //all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
        if (col.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            //add an explosion or something
            //destroy the projectile that just caused the trigger collision
            currentHealth = currentHealth - 20;
        }
    }

    public void startHealing(int pHealth)
    {
        StartCoroutine("healthOverTime", pHealth);
    }

    public void stopHealing()
    {
        StopCoroutine("healthOverTime");
    }

    IEnumerator healthOverTime(int pHealth)
    {
        while (true)
        {
            print("healing");
            currentHealth += pHealth;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth == 0)
            {
                Debug.Log("dead");
                SendMessage("OnDeath", "You Die");
            }

            yield return new WaitForSeconds(1);
        }
    }

    //public void ShootingDamage()
    //{
    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        stopHealing();
    //        currentHealth = currentHealth - 20;
    //    }
    //    else
    //    {
    //        startHealing(3);
    //    }
    //}
}

