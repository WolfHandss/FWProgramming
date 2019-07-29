using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;               // The amount of health the player starts the game with.
    public int currentHealth;                 // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.


    bool isDead = false;                                        // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.


    // Use this for initialization
    void Start()
    {
        // Set the initial health of the player.
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    healthSlider.value = currentHealth;
    //    // If the player has just been damaged...
    //    if (damaged)
    //    {
    //        // Reset the damaged flag.
    //        damaged = false;
    //    }
    //    // Otherwise...
    //    else
    //    {

    //    }


    //}

    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && isDead == false)
        {
            Death();
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        //set cursor to visible
        //Cursor.lockState = false;
        //Cursor.visible = true;

        //go to gameover scene
        SceneManager.LoadScene(2);

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


  
}

