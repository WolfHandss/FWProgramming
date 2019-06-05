using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLaserScript : MonoBehaviour {

    public GameObject laserPreFab;
    public GameObject firePoint;

    private GameObject spawnedLaser;

    public float damage = 10f;
    public float range = 100f;

    enemyHealth beamDamage;

    public Camera fpsCam;
	
	void Start ()
    {
        spawnedLaser = Instantiate(laserPreFab, firePoint.transform) as GameObject;
        DisableLaser();
        beamDamage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyHealth>();
    }
	
	
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            EnableLaser();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }




	}

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
    }

    void UpdateLaser()
    {
        if(firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }


    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
    }



    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit ))
        {
            Debug.Log(hit.transform.name);
            if(hit.collider.CompareTag("enemy"))
            {
                beamDamage.startHealing(-10);
            }

            if(hit.collider.CompareTag("enemy") == false)
            {
                beamDamage.stopHealing();
            }
        }
    }

}






