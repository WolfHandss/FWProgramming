using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

    public Material InDanger;
    public Material Idle;
    public Material LowHealth;
    private Material currentMaterial;
    private Material futureMaterial;

    private List<Renderer> Materials = new List<Renderer>();

    private float smoothing = 0;
    private float t;
    private float startTime;
    public float speed = 1.0f;

    PlayerHealth health;

    private bool inCombat = false;
    private bool changeMat = false;


	// Use this for initialization
	void Start () {
        startTime = Time.time;

        Renderer[] temp = GetComponentsInChildren<Renderer>();

        foreach(Renderer oof in temp)
        {
            Materials.Add(oof);
        }
        currentMaterial = Idle;
        health = GetComponentInParent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if(changeMat)
        {
            t = (Time.time - startTime) * speed;
            for (int i = 0; i < Materials.Count; i++)
            {
                Materials[i].material.Lerp(currentMaterial, futureMaterial, t);
            }
        }
        if(Time.time - startTime == 1)
        {
            currentMaterial = futureMaterial;
            changeMat = false;
        }
        
    }

    void InDangerState()
    {
        startTime = Time.time;
        futureMaterial = InDanger;
        changeMat = true;
    }

    void IdleState()
    {
        startTime = Time.time;
        futureMaterial = Idle;
        changeMat = true;
    }

    void LowHealthState()
    {
        startTime = Time.time;
        futureMaterial = LowHealth;
        changeMat = true;
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
            IdleState();
            inCombat = false;
        }
    }
}
