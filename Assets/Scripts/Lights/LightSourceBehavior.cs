// Amelia Nehring
// GDD 200-001
// Fall 2024

using UnityEngine;
public class LightSourceBehavior : MonoBehaviour
{
    public BatteryManager batteryManager;
    int lightLevel;
    // Start is called before the first frame update
    void Start()
    {
        //Finds the battery object and its component
        batteryManager = GameObject.Find("Battery").GetComponent<BatteryManager>();
        if (gameObject.CompareTag("Flashlight"))
        {
            lightLevel = 1;
        }
        if (gameObject.CompareTag("Candle"))
        {
            lightLevel = 2;
        }
        if (gameObject.CompareTag("Fireflies"))
        {
            lightLevel = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLightCharge();

    }

    //Checks the battery charge, and destroys the light if it has no charge
    void CheckLightCharge()
    {
        if (batteryManager.batteryCharge <= 0f)
        {
            Destroy(gameObject);
        }
    }
}