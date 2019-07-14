using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public GameObject HealthBarUI;
    public Slider slider;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
	}
	
	// Update is called once per frame
	void Update ()
    {        
        
	}

    //determine health percentage to adjust slider
    private float CalculateHealth()
    {
        return health / maxHealth;
    }

    //changes this objects health
    public void AdjustHealth(int change)
    {
        health += change;

        //keep health within certain range, -1 so player can die
        health = Mathf.Clamp(health, -1, maxHealth);

        //only implement this logic when there is a change in health
        //otherwise it's a waste of CPU constantly checking every frame for these things
        if (health < maxHealth)
        {
            HealthBarUI.SetActive(true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //only change slider when there is a change to overall health
        slider.value = CalculateHealth();
    }

    //initiate Coroutines
    public void StartHealing(int pHealth)
    {
        StartCoroutine("healthOverTime", pHealth);
    }

    public void StopHealing()
    {
        StopCoroutine("healthOverTime");
    }


    IEnumerator HealthOverTime(int pHealth)
    {
        while (true)
        {
            print("healing");
            health += pHealth;

            health = Mathf.Clamp(health, 0, maxHealth);

            yield return new WaitForSeconds(1);
        }
    }    
}
