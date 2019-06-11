using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLaserScript : MonoBehaviour {

    public GameObject laserPreFab;
    public GameObject firePoint;

    private GameObject spawnedLaser;

    public float damage = 10f;
    public float range = 100f;
   

    public Camera fpsCam;
	
	void Start ()
    {
        spawnedLaser = Instantiate(laserPreFab, firePoint.transform) as GameObject;
        DisableLaser();
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
        //shoot
        GameObject tempBullet = Instantiate(laserPreFab, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        
        Destroy(tempBullet, 2.0f);

    }






}






