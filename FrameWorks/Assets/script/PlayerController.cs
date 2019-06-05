using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpHeight = 1f;
    public float dashDistance;

    public bool isGrounded;

    Rigidbody rb;

    public ParticleSystem psLeft; // Particle system when dash left
    public ParticleSystem psRight; // Particle system when dash right

    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
        psLeft.Pause();
        psRight.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Dash();
        

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void PlayerMovement()
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

    void Dash()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.Lerp(transform.position, Vector3.left * dashDistance, 2.0f), Space.Self);
            psLeft.Play();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.Lerp(transform.position, Vector3.right * dashDistance, 2.0f), Space.Self);
            psRight.Play();
        }

    }
}
