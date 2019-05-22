using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSource : MonoBehaviour {
    public GameObject fairy;
    public GameObject colour;
    public GameObject Ogc;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("light"))
        {
            colourchange();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        OgColour();
    }
    /* private void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.CompareTag("ground"))
         {
             OgColour();
         }
     }*/
    private void colourchange()
    {
        fairy.GetComponent<Renderer>().material = colour.GetComponent<Renderer>().sharedMaterial;
    }
    private void OgColour()
    {
        fairy.GetComponent<Renderer>().material = Ogc.GetComponent<Renderer>().sharedMaterial;
    }
    // Use this for initialization
    
}
