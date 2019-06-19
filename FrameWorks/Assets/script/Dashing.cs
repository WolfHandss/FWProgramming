using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Speed = 20.0f;

    HP_Reduce m_reduce;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();

        //Set the HP_Reduce the GameObject
        m_reduce = GetComponent<HP_Reduce>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            //Move the Rigidbody to the right constantly at speed you define (the red arrow axis in Scene view)
            m_Rigidbody.velocity = transform.right * m_Speed;
            m_reduce.HPReduce();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_Rigidbody.velocity = Vector3.zero;
        }


        // if (Input.GetKey(KeyCode.LeftArrow))
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            //Move the Rigidbody to the left constantly at the speed you define (the red arrow axis in Scene view)
            m_Rigidbody.velocity = -transform.right * m_Speed;
            m_reduce.HPReduce();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_Rigidbody.velocity = Vector3.zero;
        }

        //  transform.forward
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
            //rotate the sprite about the Z axis in the positive direction
            // transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * m_Speed, Space.World);
            m_Rigidbody.velocity = transform.forward * m_Speed;
            m_reduce.HPReduce();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            m_Rigidbody.velocity = Vector3.zero;
        }

        //-   transform.forward
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            //rotate the sprite about the Z axis in the negative direction
            // transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * m_Speed, Space.World);
            m_Rigidbody.velocity = -transform.forward * m_Speed;
            m_reduce.HPReduce();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_Rigidbody.velocity = Vector3.zero;
        }
    }
}
