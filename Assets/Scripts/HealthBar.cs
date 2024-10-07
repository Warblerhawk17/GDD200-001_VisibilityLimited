using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    float maxHealth;
    public GameObject player;
    float decayValue;
    private float currentHealth;
    void Start()
    {
        maxHealth = 25.0f;
        currentHealth = maxHealth;
        decayValue = .1f;


    }

    // Update is called once per frame
    void Update()
    {
        constantDecrease(decayValue);

    }

    void constantDecrease(float decayValue)
    {
        if (currentHealth > 0)
        {
            // Transform x position at a rate half as fast as the scale
            transform.position = new Vector3(transform.position.x - decayValue * 0.5f * Time.deltaTime, transform.position.y, transform.position.z);

            // Transform x scale
            transform.localScale = new Vector3(transform.localScale.x - decayValue * Time.deltaTime, transform.localScale.y, transform.localScale.z);

            //Decrease health value
            currentHealth = currentHealth - (decayValue) ;
        }
    }

}
