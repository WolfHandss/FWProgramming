using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] private float bulletDamage;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("bullet"))
        {
            health += bulletDamage;
        }
    }
}
