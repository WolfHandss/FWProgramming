using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastAbility : MonoBehaviour {


	public float maxSize;
	public float growFactor;
	public float waitTime;
	public bool done = false;
	private void OnCollisionEnter(Collision c)
	{
		float kbForce = 5;
		if (c.gameObject.tag == "Enemy")
		{
			// Calculate Angle Between the collision point and the player
			Vector3 dir = c.contacts[0].point - transform.position;
			// We then get the opposite (-Vector3) and normalize it
			dir = -dir.normalized;
			// And finally we add force in the direction of dir and multiply it by force. 
			// This will push back the player
			GetComponent<Rigidbody>().AddForce(dir * kbForce);
		}
	}

	// Use this for initialization
	void Start () {
		
	}



	public IEnumerator Scale()
	{
		done = false;
		float timer = 0;

		while (true) // this could also be a condition indicating "alive or dead"
		{
			// we scale all axis, so they will have the same value, 
			// so we can work with a float instead of comparing vectors
			if(!done)
			{


				while (maxSize > transform.localScale.x)
				{
					timer += Time.deltaTime;
					transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
					yield return null;
				}
				// reset the timer

				yield return new WaitForSeconds(waitTime);
				transform.localScale = new Vector3(1, 1, 1);
				done = true;

			}

			//timer = 0;
			//while (1 < transform.localScale.x)
			//{
			//	timer += Time.deltaTime;
			//	transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
			//	yield return null;
			//}

			//timer = 0;
			//yield return new WaitForSeconds(waitTime);
			
			
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
