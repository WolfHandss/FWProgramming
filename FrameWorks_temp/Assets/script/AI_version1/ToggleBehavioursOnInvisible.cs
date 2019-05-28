using UnityEngine;
using System.Collections;

public class ToggleBehavioursOnInvisible : MonoBehaviour
{
	public Behaviour[] behavioursEnabledOnInvisible;
	public Behaviour[] behavioursDisabledOnInvisible;
	private bool visible = false;
	private bool raycastable = false;

	void Update()
	{
		RaycastHit hit;

   

        if (Physics.Raycast(transform.position, Camera.main.transform.position - transform.position, out hit))
		{
          

            if (hit.collider.tag == "MainCamera")
			{
                Debug.Log("True");
                raycastable = true;
			}
			else
			{
                Debug.Log("FAlse");
                raycastable = false;
			}
         
        }
		else
		{
           
            raycastable = false;

           
        }

    
    }

	void OnBecameInvisible()
	{
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        visible = false;
	}

	void OnBecameVisible()
	{
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        visible = true;
	}
}
