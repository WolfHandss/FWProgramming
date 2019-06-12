// Phil James - Capstone 2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    public float damage = 2f;
    public float radius = 1f;
    float force = 30;
    
    void OnTriggerEnter(Collider target)
    {

        // after we touch an enemy deactivate game object
        if (target.tag == Tags.PLAYER_TAG)
        {

            target.GetComponent<HealthScript>().ApplyDamage(damage);

            //  gameObject.SetActive(false);


            Vector3 dir = target.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);

        }

    }
} // class




























