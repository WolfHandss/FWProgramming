using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] private int damage = -10;
    [SerializeField] private float range = 100.0f;
    
    public Camera fpsCam;
    private enemyHealth enemyHit;
    
    void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            //targeting.StopHealing();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        //shoot the ray cast from this object, or slightly in front of it...
        Physics.Raycast(transform.position, transform.forward, out hit, range);

        if(hit.collider)
        {
            Debug.Log("Raycast is hitting stuff");
            Debug.Log(hit.collider.tag);
        }

        //if the raycast hits an enemy with Enemy Tag
        if(hit.collider.transform.tag == "Enemy")
        {
            hit.transform.GetComponent<enemyHealth>().AdjustHealth(-1);

            //draw the ray on the screen
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
            Debug.Log("raycast working");
        }
    }
}
