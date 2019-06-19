using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private float ogSpeed = 15;
    public float jumpHeight = 1f;
    public float speedRed;

    public bool isGrounded;

    Rigidbody rb;

    void Start()
    {
        
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            isGrounded = false;

            if (!isGrounded)
            {
                jumpMove();
            }
        }
        if (isGrounded)
        {
            print("LAND!!!!");
            landedSpeed();
        }

        
    }

    void PlayerMove()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
       
    }

    void OnCollisionEnter()
    {
       
        isGrounded = true;
    }
    void jumpMove()
    {
        speed /= 2;
    }

    void landedSpeed()
    {
        if (Input.GetButton("Fire1"))
        {
            speed = ogSpeed - speedRed;
        }
        else
        {
            speed = ogSpeed;
        }
    }

}
