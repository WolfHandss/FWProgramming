using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

    public Material Attack;
    public Material Idle;
    public Material LowHealth;
    private Material CurrentMaterial;

    PlayerHealth health;

	// Use this for initialization
	void Start () {
        CurrentMaterial = GetComponentInChildren<Renderer>().material;
        CurrentMaterial = Idle;

        health = GetComponentInParent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AttackState()
    {
        CurrentMaterial = Attack;
    }

    void IdleState()
    {
        CurrentMaterial = Idle;
    }

    void LowHealthState()
    {
        CurrentMaterial = LowHealth;
    }

    void setState()
    {
        if (health.currentHealth <= 30)
        {
            LowHealthState();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AttackState();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                LowHealthState();
            }
        }
        else if (health.currentHealth > 30)
        {
            IdleState();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AttackState();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                IdleState();
            }
        }
    }

}
