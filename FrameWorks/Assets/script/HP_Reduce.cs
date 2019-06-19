using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Reduce : MonoBehaviour {
    public int m_Reduce = 1;

    PlayerHealth Reduce;

    // Use this for initialization
    void Start () {
        Reduce = GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	public void HPReduce() {

        Reduce.TakeDamage(m_Reduce);

    }
}
