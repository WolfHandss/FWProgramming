using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSource : MonoBehaviour {

    PlayerHealth Health;

    // Use this for initialization
    void Start()
    {
        Health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health.startHealing(5);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health.stopHealing();
        }
    }
}
