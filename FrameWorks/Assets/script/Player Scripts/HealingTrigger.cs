// Phil James - Capstone 2019
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealingTrigger : MonoBehaviour
{
    // Inspector Variables

    [SerializeField] float _healAmount = 100f;
    [SerializeField] private Image health_Stats;
    private PlayerStats player_Stats;


    int _parameterHash = -1;
   
    private bool is_HP;



    // ------------------------------------------------------------
    // Phil James - Capstone 2019
    // Desc	:	Called on object start-up to initialize the script.
    // ------------------------------------------------------------
    void Start()
    {
        player_Stats = GetComponent<PlayerStats>();
        //health_Stats = transform.FindChild("Health Foreground").GetComponent<Image>();
    }

  
    void OnTriggerStay(Collider col)
    {
     
      
        // If this is the player object and our parameter is set for damage
        if (col.gameObject.CompareTag("Player") )
        {

            health_Stats.fillAmount = _healAmount;

            Debug.Log("Light Power Up..!!");


          
                  

        }
    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
                      
            Destroy(gameObject);
            
        }
    }
}
