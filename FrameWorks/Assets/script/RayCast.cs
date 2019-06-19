using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {


    public int damage = -10;
    public float range = 100f;

    public Camera fpsCam;
    private enemyHealth targeting;
    
    void Update ()
    {
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
            else
            {
                targeting.stopHealing();
            }
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            targeting = hit.transform.GetComponent<enemyHealth>();

            Debug.Log(hit.transform.name);
            Debug.Log("raycast working");
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("raycast hitting");
                targeting.startHealing(-3);
            }
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
        }
    }
}
