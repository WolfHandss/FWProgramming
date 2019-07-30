using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float ogSpeed = 15;
    public float jumpHeight = 1f;
    public float speedRed;
    public float floatSpeed = 0.2f;
    public float airSpeedcont = 0.7f;
    public float verticalVel;
    public float gravity = 200;
    private Vector3 MoveDirec;
    public float forceMultiply = 10;
   // private float thrust = 10;
    public bool isGrounded;
    float jumpForce = 5;
    Rigidbody rb;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMulitp = 10;
    void Start()
    {
        
        isGrounded = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
            if (isGrounded)
            {
                //landedSpeed();
                PlayerMove();
            }
        else
        {
            inAirCont();

        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
            rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
                //float test = rb.velocity.z;
            //test = rb.velocity.z + test;
                //rb.velocity += (rb.velocity.x, jumpForce , rb.velocity.z);
               // rb.AddForce(0, jumpHeight, test, ForceMode.Impulse);
                //rb.AddForce(transform.up * thrust);
                isGrounded = false;
            //StartCoroutine(JumpEvent());
            //if (!isGrounded)
            //{
            //    jumpMove();
            //}
        }

        //else
        //{
        //    inAirCont();
        //}
        //applyGrav();
    }
    
    void PlayerMove()
    {
        

        ////if (isGrounded)
        ////{
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
        Vector3 forwardMove = transform.forward * ver;
        Vector3 HorMove = transform.right * hor;
           //Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed*Time.deltaTime;
           
        Vector3 horizontal = rb.velocity;
        horizontal.y = 0;
        if(horizontal.magnitude < ogSpeed)
        {
            rb.AddForce((forwardMove + HorMove) * forceMultiply, ForceMode.VelocityChange);
        }
        horizontal = Mathf.Min(horizontal.magnitude, ogSpeed) * horizontal.normalized;
        rb.velocity = horizontal + rb.velocity.y * Vector3.up;

        if (hor == 0)
        {
            Vector3 sidewaysVel = Vector3.Dot(rb.velocity, transform.right) * transform.right;
            rb.velocity -= sidewaysVel;
        }

        if (ver == 0)
        {
            Vector3 forwardVel = Vector3.Dot(rb.velocity, transform.forward) * transform.forward;
            rb.velocity -= forwardVel;
        }
        //}
        //else
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        //}
        //else
        //{

        //    //Vector3 playerMovement = new Vector3(hor/, 0f, ver) * speed * Time.deltaTime;
        //   // transform.Translate(playerMovement, Space.Self);
        //}
    }

    void OnCollisionEnter()
    {
       
        isGrounded = true;
    }
    //void jumpMove()
    //{
    //    speed /= 2;
    //}

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
    void inAirCont()
    {
        //float hor = Input.GetAxis("Horizontal");
        //float ver = Input.GetAxis("Vertical");
        //Vector3 forwardMove = transform.forward * ver;
        //Vector3 HorMove = transform.right * hor;
        ////ver = Mathf.Clamp(ver, -0.2f, 1);
        ////ver = ver * 0.5f;
        ////hor = hor * 0.5f;
        //Vector3 playerMovement = new Vector3(hor, 0f, ver) * airSpeedcont * Time.deltaTime;
        //rb.AddForce((forwardMove + HorMove) * 5, ForceMode.Impulse);
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 forwardMove = transform.forward * ver;
        Vector3 HorMove = transform.right * hor;
        //Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed*Time.deltaTime;

        Vector3 horizontal = rb.velocity;
        horizontal.y = 0;
        if (horizontal.magnitude < airSpeedcont)
        {
            rb.AddForce((forwardMove + HorMove) * forceMultiply, ForceMode.VelocityChange);
        }
        horizontal = Mathf.Min(horizontal.magnitude, airSpeedcont) * horizontal.normalized;
        rb.velocity = horizontal + rb.velocity.y * Vector3.up;

        if (hor == 0)
        {
            Vector3 sidewaysVel = Vector3.Dot(rb.velocity, transform.right) * transform.right;
            rb.velocity -= sidewaysVel;
        }

        if (ver == 0)
        {
            Vector3 forwardVel = Vector3.Dot(rb.velocity, transform.forward) * transform.forward;
            rb.velocity -= forwardVel;
        }
    }
    //private IEnumerator JumpEvent()
    //{
    //    float timeInAir = 0.0f;
    //    do
    //    {
    //        jumpForce = jumpFallOff.Evaluate(timeInAir);
    //        Vector3 playerMovement = Vector3.up * jumpForce *jumpMulitp* Time.deltaTime;
    //        transform.Translate(playerMovement, Space.Self);
    //        timeInAir += Time.deltaTime;
    //        yield return null;
    //    }
    //    while (!isGrounded);
       
    //}
    //void applyGrav()
    //{
    //    verticalVel -= gravity * Time.deltaTime;

    //    MoveDirec.y = verticalVel * Time.deltaTime;
    //}
}
