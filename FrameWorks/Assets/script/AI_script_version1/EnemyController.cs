//Name = Phil James
//Capstone 2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : MonoBehaviour {

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_Point;

    private EnemyAudio enemy_Audio;

    //PlayerHealth damage;

    //++++++++++++++++++++++++++++++++++++
    // Inspector Assigned Variable
    public AIWaypointNetwork WaypointNetwork = null;
    public int CurrentIndex = 0;
    public bool HasPath = false;
    public bool PathPending = false;
    public bool PathStale = false;
    public UnityEngine.AI.NavMeshPathStatus PathStatus = UnityEngine.AI.NavMeshPathStatus.PathInvalid;
    public AnimationCurve JumpCurve = new AnimationCurve();

    private float _originalMaxSpeed = 0;


    void Awake() {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enemy_Audio = GetComponentInChildren<EnemyAudio>();


    }

    // Use this for initialization
    void Start () {
       // damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        enemy_State = EnemyState.PATROL;

        patrol_Timer = patrol_For_This_Time;

        // when the enemy first gets to the player
        // attack right away
        attack_Timer = wait_Before_Attack;

        // memorize the value of chase distance
        // so that we can put it back
        current_Chase_Distance = chase_Distance;


        // If not valid Waypoint Network has been assigned then return
        if (WaypointNetwork == null) return;


        // Set first waypoint
        SetNextDestination(false);
    }
	
	// Update is called once per frame
	void Update () {
        SetNewRandomDestination();
        if (enemy_State == EnemyState.PATROL) {
            Patrol();
        }

        if(enemy_State == EnemyState.CHASE) {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }

    }
    // -----------------------------------------------------
    // Programmer : Phil
    // Desc	:	Optionally increments the current waypoint
    //			index and then sets the next destination
    //			for the agent to head towards.
    // -----------------------------------------------------
    void SetNextDestination(bool increment)
    {
        // If no network return
        if (!WaypointNetwork) return;

        // Calculatehow much the current waypoint index needs to be incremented
        int incStep = increment ? 1 : 0;
        Transform nextWaypointTransform = null;

        // Calculate index of next waypoint factoring in the increment with wrap-around and fetch waypoint 
        int nextWaypoint = (CurrentIndex + incStep >= WaypointNetwork.Waypoints.Count) ? 0 : CurrentIndex + incStep;
        nextWaypointTransform = WaypointNetwork.Waypoints[nextWaypoint];

        // Assuming we have a valid waypoint transform
        if (nextWaypointTransform != null)
        {
            // Update the current waypoint index, assign its position as the NavMeshAgents
            // Destination and then return
            CurrentIndex = nextWaypoint;
           navAgent.destination = nextWaypointTransform.position;
            return;
        }

        // We did not find a valid waypoint in the list for this iteration
        CurrentIndex = nextWaypoint;
    }


    void Patrol() {

        // tell nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_For_This_Time) {

            // If we don't have a path and one isn't pending then set the next
            // waypoint as the target, otherwise if path is stale regenerate path
            if ((navAgent.remainingDistance <= navAgent.stoppingDistance && !PathPending) || PathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid /*|| PathStatus==NavMeshPathStatus.PathPartial*/)
            {
                SetNextDestination(true);
            }
            else
            if (navAgent.isPathStale)
                SetNextDestination(false);

            patrol_Timer = 0f;

        }

        if(navAgent.velocity.sqrMagnitude > 0) {
        
            enemy_Anim.Walk(true);
        
        } else {

            enemy_Anim.Walk(false);

        }

        // test the distance between the player and the enemy
        if(Vector3.Distance(transform.position, target.position) <= chase_Distance) {

            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;

            Debug.Log("AI Enemy State = CHASE!!");
            // play spotted audio
            enemy_Audio.Play_ScreamSound();

        }


    } // patrol

    void Chase() {

        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        Debug.Log("AI Enemy CHASING! the Player Now!");
        // set the player's position as the destination
        // because we are chasing(running towards) the player
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0) {

            enemy_Anim.Run(true);

        } else {

            enemy_Anim.Run(false);

        }

        // if the distance between enemy and player is less than attack distance
        if(Vector3.Distance(transform.position, target.position) <= attack_Distance) {

            // stop the animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            Debug.Log("AI Enemy ATTACK!!");

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }

        } else if(Vector3.Distance(transform.position, target.position) > chase_Distance) {
            // player run away from enemy

            // stop running
            enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            Debug.Log("AI Enemy State = PATROL!!");
            // reset the patrol timer so that the function
            // can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }


        } // else

    } // chase

    void Attack() {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack) {

            enemy_Anim.Attack();

            //damage.TakeDamage(10);

            attack_Timer = 0f;

            // play attack sound
            enemy_Audio.Play_AttackSound();

        }

        if(Vector3.Distance(transform.position, target.position) >
           attack_Distance + chase_After_Attack_Distance) {

            enemy_State = EnemyState.CHASE;

            Debug.Log("AI Enemy State = CHASE!!");
        }


    } // attack

    void SetNewRandomDestination() {

        //float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        //Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        //randDir += transform.position;

        //NavMeshHit navHit;

        //NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        //navAgent.SetDestination(navHit.position);

        int turnOnSpot;

        // Copy NavMeshAgents state into inspector visible variables
        HasPath = navAgent.hasPath;
        PathPending = navAgent.pathPending;
        PathStale = navAgent.isPathStale;
        PathStatus = navAgent.pathStatus;

        // Perform corss product on forard vector and desired velocity vector. If both inputs are Unit length
        // the resulting vector's magnitude will be Sin(theta) where theta is the angle between the vectors.
        Vector3 cross = Vector3.Cross(transform.forward, navAgent.desiredVelocity.normalized);

        // If y component is negative it is a negative rotation else a positive rotation
        float horizontal = (cross.y < 0) ? -cross.magnitude : cross.magnitude;

        // Scale into the 2.32 range for our animator
        horizontal = Mathf.Clamp(horizontal * 2.32f, -2.32f, 2.32f);

        // If we have slowed down and the angle between forward vector and desired vector is greater than 10 degrees 
        if (navAgent.desiredVelocity.magnitude < 1.0f && Vector3.Angle(transform.forward, navAgent.steeringTarget - transform.position) > 10.0f)
        {
            // Stop the nav agent (approx) and assign either -1 or +1 to turnOnSpot based on sign on horizontal
            navAgent.speed = 10f;
            turnOnSpot = (int)Mathf.Sign(horizontal);
        }
        else
        {
            // Otherwise it is a small angle so set Agent's speed to normal and reset turnOnSpot
            navAgent.speed = _originalMaxSpeed;
            turnOnSpot = 0;
        }

        // Programmer :  Phil
        // This is for NPC
        // If agent is on an offmesh link then perform a jump
        /*if (_navAgent.isOnOffMeshLink)
		{
			StartCoroutine( Jump( 1.0f) );
			return;
		}*/

       
    }

    void Turn_On_AttackPoint() {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint() {
        if (attack_Point.activeInHierarchy) {
            attack_Point.SetActive(false);
        }
    }

    public EnemyState Enemy_State {
        get; set;
    }

    // ---------------------------------------------------------
    // Name	:	Jump
    // Desc	:	Manual OffMeshLInk traversal using an Animation
    //			Curve to control agent height.
    // ---------------------------------------------------------
    IEnumerator Jump(float duration)
    {
        // Get the current OffMeshLink data
        UnityEngine.AI.OffMeshLinkData data = navAgent.currentOffMeshLinkData;

        // Start Position is agent current position
        Vector3 startPos = navAgent.transform.position;

        // End position is fetched from OffMeshLink data and adjusted for baseoffset of agent
        Vector3 endPos = data.endPos + (navAgent.baseOffset * Vector3.up);

        // Used to keep track of time
        float time = 0.0f;

        // Keeo iterating for the passed duration
        while (time <= duration)
        {
            // Calculate normalized time
            float t = time / duration;

            // Lerp between start position and end position and adjust height based on evaluation of t on Jump Curve
            navAgent.transform.position = Vector3.Lerp(startPos, endPos, t) + (JumpCurve.Evaluate(t) * Vector3.up);

            // Accumulate time and yield each frame
            time += Time.deltaTime;
            yield return null;
        }

        // NOTE : Added this for a bit of stability to make sure the
        //        Agent is EXACTLY on the end position of the off mesh
        //		  link before completeing the link.
        navAgent.transform.position = endPos;

        // All done so inform the agent it can resume control
        navAgent.CompleteOffMeshLink();
    }
} // class


































