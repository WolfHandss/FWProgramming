using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {


    //enemyHealth damage;
	// Use this for initialization
	void Start () {
        //damage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealth damage = collision.gameObject.GetComponent<enemyHealth>();
            damage.DoDamage(-3);
        }
    }

}
