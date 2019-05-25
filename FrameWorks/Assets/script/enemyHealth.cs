using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {
    public float health;
    public float maxHealth;

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
}
