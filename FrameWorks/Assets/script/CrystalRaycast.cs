using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRaycast : MonoBehaviour
{

    [SerializeField] private float range = 100.0f;

    public bool activate = false;

    void Update()
    {

    }
    public void Shoot()
    {
        RaycastHit hit;
        //shoot the ray cast from this object, or slightly in front of it...
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            //if the raycast hits an enemy with crystal Tag
            if (hit.collider.CompareTag("CrystalRaycast"))
            {
                hit.collider.GetComponent<CrystalRaycast>().Shoot();
            }
            
        }
        Debug.DrawRay(transform.position, transform.forward* 100, Color.green);
        Debug.Log("casting");
    }
}
