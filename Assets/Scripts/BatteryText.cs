using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryText : MonoBehaviour
{
    // Start is called before the first frame update
    public BatteryManager batteryManager;
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Charge: " + batteryManager.batteryCharge);
    }
}
