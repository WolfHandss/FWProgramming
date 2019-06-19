using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBlast : MonoBehaviour {

	public BlastAbility blasty;

	// Use this for initialization
	void Start () {
		blasty = GetComponent<BlastAbility>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton(0))
		{
			Instantiate(gameObject);

			StartCoroutine(blasty.Scale());

			if (blasty.done)
			{
				StopCoroutine(blasty.Scale());
			}
			Destroy(gameObject);

		}
	}
}
