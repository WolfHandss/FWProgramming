using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour
{
	public float speed = 1.0f;

	private GameObject player;

    //public Transform other;



	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (player)
        {

            Debug.Log("Boo Mechanic Activated - enemy moving now.");
            transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
        
        }
    }

  //  Handles.color = Color.white;
		//Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		//Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
  //  Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

  //  Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA* fow.viewRadius);
  //  Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB* fow.viewRadius);

  //  Handles.color = Color.red;
		//foreach (Transform visibleTarget in fow.visibleTargets) {
		//	Handles.DrawLine(fow.transform.position, visibleTarget.position);
		//}
}
