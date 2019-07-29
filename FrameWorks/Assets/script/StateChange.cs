using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

    public Material InDanger;
    public Material Idle;
    public Material LowHealth;
    private Material currentMaterial;
    public List<Renderer> Materials = new List<Renderer>();

    public float smoothing = 1;

    PlayerHealth health;

    private bool inCombat = false;

	// Use this for initialization
	void Start () {
        Renderer[] temp = GetComponentsInChildren<Renderer>();

        foreach(Renderer oof in temp)
        {
            Materials.Add(oof);
        }
        currentMaterial = Idle;
        //InDangerState();
        health = GetComponentInParent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        setState();
	}

    void InDangerState()
    {
        for (int i = 0; i < Materials.Count; i++)
        {
            Materials[i].material.Lerp(currentMaterial, InDanger, smoothing);            
        }
        currentMaterial = InDanger;
    }

    void IdleState()
    {
        for (int i = 0; i < Materials.Count; i++)
        {
            Materials[i].material.Lerp(currentMaterial, Idle, smoothing);
        }
        currentMaterial = Idle;
    }

    void LowHealthState()
    {
        for (int i = 0; i < Materials.Count; i++)
        {
            Materials[i].material.Lerp(currentMaterial, LowHealth, smoothing);
        }
        currentMaterial = LowHealth;
    }

    void setState()
    {
        if (!inCombat)
        {
            if (health.currentHealth <= 30)
            {
                LowHealthState();
            }
            else if (health.currentHealth > 30)
            {
                IdleState();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            InDangerState();
            inCombat = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            setState();
            inCombat = false;
        }
    }
}
