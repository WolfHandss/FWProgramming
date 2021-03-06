﻿using UnityEngine;
using System.Collections;
// ----------------------------------------------------------
// Capstone	:	Iron Light
// DESC		:	AI Behaviour to test Unity's Vector3 and
//				Animator.
// STUDENT  :   PHIL JAMES LAPUZ
// ----------------------------------------------------------
public class chase : MonoBehaviour
{

    public Transform player;
    public Transform head;
    static Animator anim;
    bool pursuing = false;


    // Use this for initialization
    void Start()
    {
      //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        float angle = Vector3.Angle(direction, head.up);

    

        if (Vector3.Distance(player.position, this.transform.position) < 10 && (angle <= 90 || pursuing))
        {
          
            pursuing = true;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(direction), 0.1f);

       //     anim.SetBool("isIdle", false);
            if (direction.magnitude > 5)
            {
                this.transform.Translate(0, 0, 0.05f);
             //  anim.SetBool("isWalking", true);
             //   anim.SetBool("Attack", false);
            }
            else
            {
            //   anim.SetBool("Attack", true);
              // anim.SetBool("isWalking", false);
            }

        }
        else
        {
        //    anim.SetBool("isIdle", true);
        //    anim.SetBool("isWalking", false);
        //    anim.SetBool("isAttacking", false);
            pursuing = false;
        }

    }
}
