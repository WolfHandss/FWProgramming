using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {


    public int damage = -10;
    public float range = 100f;

    public Camera fpsCam;

	void Start ()
    {
        
       
    }
	void Update ()
    {
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }


        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
           enemyHealth target = hit.transform.GetComponent<enemyHealth>();
            if (target != null)
            {
                Debug.Log("hitting");
                target.startHealing(-3);
            }
            else
            {
                target.stopHealing();
            }
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
        }
    }
}
