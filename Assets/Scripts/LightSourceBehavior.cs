using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceBehavior : MonoBehaviour
{
    public BatteryManager batteryManager;
    // Start is called before the first frame update
    void Start()
    {
        batteryManager = GameObject.Find("Battery").GetComponent<BatteryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLightCharge();
        
    }

    void CheckLightCharge()
    {
        if (batteryManager.batteryCharge <= 0f)
        {
            Destroy(gameObject);
        }
    }
}