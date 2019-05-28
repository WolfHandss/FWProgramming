using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase, Attack }
    public Transform[] wayPoints;
    public Transform targetPlayer;
    public float maxDamage = 10f;
    public PlayerHealth playerHealth;

    public EnemyState CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;

            //stop all running coroutines
            StopAllCoroutines();

            switch (currentState)
            {
                case EnemyState.Patrol:
                    StartCoroutine(EnemyPatrol());
                    Debug.Log("currentState = " + "Patrol");
                    break;
                case EnemyState.Chase:
                    StartCoroutine(EnemyChase());
                    Debug.Log("currentState = " + "Chase");
                    break;
                case EnemyState.Attack:
                    StartCoroutine(EnemyAttack());
                    Debug.Log("currentState = " + "Attack");
                    break;
            }
        }
    }


    [SerializeField]
    private EnemyState currentState = EnemyState.Patrol;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private int wayPointIndex = 0;
    private EnemySight.Sensitivity sensitivty;
    private EnemySight enemySight;
    private Animator animator;

    void Awake()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemySight = GetComponent<EnemySight>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        CurrentState = EnemyState.Patrol;
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator EnemyPatrol()
    {
        while (currentState == EnemyState.Patrol)
        {
            navAgent.stoppingDistance = 0;
            //print("Patrolling");

            //do not see through the obstacles
            sensitivty = EnemySight.Sensitivity.Strict;

            // Chase state to patrol state
            navAgent.Resume();
            navAgent.SetDestination(wayPoints[wayPointIndex].position);

            // Wait till path is computed
            while (navAgent.pathPending)
                yield return null;

            // Check how close the agent is to destination
            if (navAgent.remainingDistance < 0.5f)
            {
                // Go to next frame if there are no waypoints
                if (wayPoints.Length == 0)
                    yield return null;

                // Move the Navmesh agent per Waypoint by index number
                navAgent.destination = wayPoints[wayPointIndex].position;
                // Increase index number 
                wayPointIndex = (wayPointIndex + 1) % wayPoints.Length;

                //print(enemySight.canSeePlayer);

                // -----> GO TO CHASE STATE IF TARGET IS SEEN <------- //
                /*
				if(enemySight.canSeePlayer)	{
					print(enemySight.canSeePlayer);
					break;
				}*/


            }
            if (enemySight.canSeePlayer)
            {
                CurrentState = EnemyState.Chase;

            }
            yield return null;
        }
    }

    IEnumerator EnemyChase()
    {
        while (currentState == EnemyState.Chase)
        {
            navAgent.stoppingDistance = 2;
            //look behind obstacles
            sensitivty = EnemySight.Sensitivity.Loose;

            navAgent.Resume();
            navAgent.SetDestination(enemySight.LastKnowSighting);

            // Wait till path is computed
            while (navAgent.pathPending)
                yield return null;


            if (navAgent.remainingDistance < navAgent.stoppingDistance)
            {
                navAgent.Stop();

                CurrentState = EnemyState.Attack;
            }


            if (!enemySight.canSeePlayer)
            {
                CurrentState = EnemyState.Patrol;

            }

            yield return null;
        }
    }

    IEnumerator EnemyAttack()
    {
        while (currentState == EnemyState.Attack)
        {

            // Wait till path is computed
            while (navAgent.pathPending)
                yield return null;

            //playerHealth.Health(maxDamage);
            //print(health -= maxDamage * Time.deltaTime);
            navAgent.SetDestination(enemySight.LastKnowSighting);
            Vector3 relPos = targetPlayer.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relPos);
            transform.rotation = rotation;
            if (navAgent.remainingDistance > navAgent.stoppingDistance)
            {

                CurrentState = EnemyState.Chase;
            }
            else
            {
                playerHealth.Health -= maxDamage * Time.deltaTime;
            }



            yield return null;

        }
    }
}