using UnityEngine;
using System.Collections;

public class ShootingObject : MonoBehaviour
{
    //Spawn Point
    public GameObject Bullet_Emitter;

    //Object 
    public GameObject Bullet;
    //Force
    public float Bullet_Forward_Force;
    //Bullet Damage
    //public int bulletDmg = 20;


    private void Start()
    {
    }

    public virtual void Shooting()
    {

        //The Bullet instantiation happens here.
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

        //Basic Clean Up, set the Bullets to self destruct after 1 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(Temporary_Bullet_Handler, 1.0f);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shooting();
        }
    }
}
