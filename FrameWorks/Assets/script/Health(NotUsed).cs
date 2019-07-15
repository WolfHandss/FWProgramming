using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] public int currentHealth;
    [SerializeField] private int maxHealth;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int pHealth)
    {
        print("healthboost");
        currentHealth += pHealth;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth == 0)
        {
            Debug.Log("dead");
            SendMessage("OnDeath", "You Die");
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("lightSource"))
    //    {
    //        while (currentHealth < maxHealth)
    //        {
    //            //print("healthboost");
    //            //colourchange();
    //            currentHealth++;
    //        }
    //    }

    //}
    public void startHealing(int pHealth)
    {
        StartCoroutine("healthOverTime", pHealth);
    }

    public void stopHealing()
    {
        StopCoroutine("healthOverTime");
    }

    IEnumerator healthOverTime(int pHealth)
    {
        while (true)
        {
            print("healing");
            currentHealth += pHealth;

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth == 0)
            {
                Debug.Log("dead");
                SendMessage("OnDeath", "You Die");
            }

            yield return new WaitForSeconds(1);
        }
    }
}

