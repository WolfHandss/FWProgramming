/// <summary>
/// EnemySight.cs
/// December 3, 2016
/// Patrick Reitsema
/// 
/// This class contains the sighting of the enemy 
/// 
/// </summary>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class EnemySight : MonoBehaviour
{

    public enum Sensitivity { Strict, Loose }
    public Sensitivity sensitivity = Sensitivity.Strict;
    public Transform[] wayPoints;
    public Transform player;
    public Transform eyePoint;

    public bool canSeePlayer = false;
    public float sightRange = 45f;
    public Vector3 LastKnowSighting;

    private UnityEngine.AI.NavMeshAgent navAgent;
    private int waypointIndex = 0;

    private Transform thisTransform;
    private SphereCollider sphereCollider;
    // Use this for initialization
    void Awake()
    {
        thisTransform = GetComponent<Transform>();
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ClearView())
        {
            Debug.Log("TRUE ClearView");
        }
        else
        { 
        if (navAgent.remainingDistance < 0.5f)
        {
            Debug.Log("TRUE OntriggerStay");
            NextWayPoint();
        }
        }
      
    }

    void NextWayPoint()
    {
        if (wayPoints.Length == 0)
            return;

        navAgent.destination = wayPoints[waypointIndex].position;
        waypointIndex = (waypointIndex + 1) % wayPoints.Length;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("OntriggerStay");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OntriggerStay");
            CheckObjectSensitivity();
        }
    }

    void OnTriggerExit()
    {
        canSeePlayer = false;
    }


    bool EnemyFieldOfView()
    {

        Vector3 directionToPlayer = player.position - eyePoint.position;
        float angle = Vector3.Angle(eyePoint.forward, directionToPlayer);

        if (angle <= sightRange)
        {
            return true;
        }
        return false;
    }

    bool ClearView()
    {
        RaycastHit hit;

         if (Physics.Raycast(eyePoint.position, (player.position - eyePoint.position).normalized, out hit, sphereCollider.radius))
         {
         
            if (hit.transform.CompareTag("Player"))
            {
                LastKnowSighting = player.transform.position;

                Debug.Log("RayCast");
                return true;
            }
        }
        return false;
    }

    void CheckObjectSensitivity()
    {
        switch (sensitivity)
        {
            case Sensitivity.Strict:
                canSeePlayer = EnemyFieldOfView() && ClearView();
                break;
            case Sensitivity.Loose:
                canSeePlayer = EnemyFieldOfView() || ClearView();
                break;
        }
    }
}