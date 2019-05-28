using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    public GameObject player;
    public Vector3[] patrolPoints; //Add in inspector

    private int patrolPoint = 0;
    private NavMeshAgent agent;

    void Patrol()
    {
        agent.Resume();
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[patrolPoint]);
            if (transform.position == patrolPoints[patrolPoint] || Vector3.Distance(transform.position, patrolPoints[patrolPoint]) < 0.2f)
            {
                patrolPoint++;    //use distance if needed(lower precision)
            }
            if (patrolPoint >= patrolPoints.Length)
            {
                patrolPoint = 0;
            }
        }
    }

    void Attack()
    {
        //??? your job...
        agent.Stop(); //maybe not needed
        Debug.Log("Whaaaaaaa");    //just to get informed
    }

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (!Physics.Linecast(transform.position, player.transform.position, 1))
        { //check if we see player by linecasting,move player to another layer so the ray won't hit it. 
            Attack();
        }
        else
        {
            Patrol();
        }

        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, Camera.main.transform.position - transform.position, out hit))
        //{

        //    if (hit.transform.CompareTag("Player"))
        //    {
        //        Attack();

        //        Debug.Log("RayCast");

        //    }
        //    else
        //    {
        //        Patrol();
        //    }
        //}
        //else
        //{
        //    Patrol();
        //}

    }

}
