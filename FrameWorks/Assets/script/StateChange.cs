using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

    public Material InDanger;
    public Material Idle;
    public Material LowHealth;
    private Material futureMaterial;

    private List<Renderer> Materials = new List<Renderer>();

    public float speed = 1.0f;

    PlayerHealth health;

    private bool inCombat = false;

	// Use this for initialization
	void Start () {

        Renderer[] temp = GetComponentsInChildren<Renderer>();

        foreach(Renderer oof in temp)
        {
            Materials.Add(oof);
        }
        health = GetComponentInParent<PlayerHealth>();

        futureMaterial = Idle;
	}
	
	// Update is called once per frame
	void Update () {

        if (futureMaterial != Materials[0].material)
        {
            for (int i = 0; i < Materials.Count; i++)
            {
                Materials[i].material.Lerp(Materials[i].material, futureMaterial, speed * Time.deltaTime);
            }
        }

        setState();
    }

    void InDangerState()
    {
        futureMaterial = InDanger;
    }

    void IdleState()
    {
        futureMaterial = Idle;
    }

    void LowHealthState()
    {
        futureMaterial = LowHealth;
    }

    void setState()
    {
        if (!inCombat)
        {
            if (health.GetHealth() <= 30)
            {
                LowHealthState();
            }
            else if (health.GetHealth() > 30)
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
