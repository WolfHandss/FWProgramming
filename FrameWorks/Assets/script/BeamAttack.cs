using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour {

	// Use this for initialization
	
    public GameObject laserPreFab;
    public GameObject firePoint;


    private GameObject spawnedLaser;

    [SerializeField] private int damage = -10;
    [SerializeField] private float range = 100.0f;

    public Camera fpsCam;
    private enemyHealth enemyHit;

    void Start()
    {
        spawnedLaser = Instantiate(laserPreFab, firePoint.transform) as GameObject;
        DisableLaser();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            //targeting.StopHealing();
        }
    }

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);

    }

    void UpdateLaser()
    {
        if (firePoint != null)
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
        //shoot the ray cast from this object, or slightly in front of it...
        Physics.Raycast(transform.position, transform.forward, out hit, range);

        if (hit.collider)
        {
            Debug.Log("Raycast is hitting stuff");
            Debug.Log(hit.collider.tag);
        }

        //if the raycast hits an enemy with Enemy Tag
        if (hit.collider.transform.tag == "Enemy")
        {
            hit.transform.GetComponent<enemyHealth>().AdjustHealth(-1);

            //draw the ray on the screen
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward, Color.green);
            Debug.Log("raycast working");
        }
    }
}
