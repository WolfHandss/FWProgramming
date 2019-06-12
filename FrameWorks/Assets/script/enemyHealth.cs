using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    //[SerializeField] private float bullet1Damage;
    //[SerializeField] private float bullet2Damage;
    //[SerializeField] private float bullet3Damage;

    public GameObject HealthBarUI;
    public Slider slider;
	// Use this for initialization
	void Start () {
        health = maxHealth;
        slider.value = CalculateHealth();
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = CalculateHealth();
        if(health < maxHealth)
        {
            HealthBarUI.SetActive(true);
        }

        if( health <=0)
        {
            Destroy(gameObject);
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
	}
    float CalculateHealth()
    {
        return health / maxHealth;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet1"))
        {
            health += bullet1Damage;
        }

        if (collision.gameObject.CompareTag("Bullet2"))
        {
            health += bullet2Damage;
        }

        if (collision.gameObject.CompareTag("Bullet3"))
        {
            health += bullet3Damage;
        }
    }*/
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
            health += pHealth;

            health = Mathf.Clamp(health, 0, maxHealth);

            yield return new WaitForSeconds(1);
        }
    }
}
