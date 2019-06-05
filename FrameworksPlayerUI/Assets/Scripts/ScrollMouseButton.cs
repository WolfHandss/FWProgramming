using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMouseButton : MonoBehaviour
{
	//public GameObject circle2Rotate;
	


    // Start is called before the first frame update
    void Start()
    {
	
    }

	

    // Update is called once per frame
    void Update()
    {
		if (Input.mouseScrollDelta.y > 0)
		{
			//circle2Rotate.transform.Rotate(Vector3.up, 90f);
			transform.Rotate(Vector3.forward, 90f);
		}
		else 
		if(Input.mouseScrollDelta.y < 0)
		{
			//circle2Rotate.transform.Rotate(Vector3.up, -90f);
			transform.Rotate(Vector3.forward, -90f);
		}
    }
}
