using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    [SerializeField] public float dashForce;
    [SerializeField] public float dashDuration;

    public float dashCoolDown = 1.0f;
    public float newDashTime = 0.0f;

   

    public float dashBonus = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > newDashTime)
        {

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift))))
            {
                if (dashBonus < 2.0f)
                {
                    dashBonus += Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift) || dashBonus >= 2f)))
            {
                StartCoroutine(CastForward());
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift))))
            {
                if (dashBonus < 2.0f)
                {
                    dashBonus += Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.A) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift) || dashBonus >= 2f)))
            {
                StartCoroutine(CastLeft());
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift))))
            {
                if (dashBonus < 2.0f)
                {
                    dashBonus += Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift) || dashBonus >= 2f)))
            {
                StartCoroutine(CastRight());
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift))))
            {
                if (dashBonus < 2.0f)
                {
                    dashBonus += Time.deltaTime;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S) || (Input.GetKey(KeyCode.A) && (Input.GetKeyDown(KeyCode.LeftShift) || dashBonus >= 2f)))
            {
                StartCoroutine(CastBack());
            }

        }
    }

    IEnumerator CastForward()
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * dashForce * dashBonus, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newDashTime = Time.time + dashCoolDown;

        dashBonus = 1.0f;
    }

    IEnumerator CastLeft()
    {
        GetComponent<Rigidbody>().AddForce(-transform.right * dashForce * dashBonus, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newDashTime = Time.time + dashCoolDown;

        dashBonus = 1.0f;
    }

    IEnumerator CastRight()
    {
        GetComponent<Rigidbody>().AddForce(transform.right * dashForce * dashBonus, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newDashTime = Time.time + dashCoolDown;

        dashBonus = 1.0f;
    }

    IEnumerator CastBack()
    {
        GetComponent<Rigidbody>().AddForce(-this.transform.forward * dashForce * dashBonus, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newDashTime = Time.time + dashCoolDown;

        dashBonus = 1.0f;
    }
}
