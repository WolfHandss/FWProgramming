using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{

    public Slider sliderValue;

    [SerializeField]
    private float health = 100f;
    // Use this for initialization

    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health >= 0)
                sliderValue.value = health;
            else
                Destroy(gameObject);
        }
    }
}