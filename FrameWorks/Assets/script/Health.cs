using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int pHealth)
    {
        currentHealth += pHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth == 0)
        {
            Debug.Log("dead");
            SendMessage("OnDeath", "You Die");
        }
    }
}

