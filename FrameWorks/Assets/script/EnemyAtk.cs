using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAtk : MonoBehaviour
{ 
    PlayerHealth damage;

    void Start()
    {
        // anim = gameObject.GetComponent<Animator>();
        //enemyNav = GetComponent<NavMeshAgent>();
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
    //    // enemyNav.SetDestination(player.transform.position);

    //    // anim.SetTrigger("Attacking");

    //    //if (enemyNav.isStopped == false)
    //    //{
    //    //    anim.SetBool("Walk", true);
    //    //}

    //    //if (anim.GetNextAnimatorStateInfo(0).IsName("Attack"))
    //    //{
    //    //    enemyNav.isStopped = true;
    //    // anim.SetBool("Attack", true);
    //    // }
    }

    //// When the weapon hits the player.
    //void OnTriggerEnter(Collider target)
    //{


    //}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            damage.TakeDamage(10);
        }
    }



}
