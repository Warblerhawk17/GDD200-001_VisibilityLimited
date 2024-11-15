// Amelia Nehring
// GDD 200-001
// Fall 2024

using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class LightSourceBehavior : MonoBehaviour
{
    public BatteryManager batteryManager;
    //int lightLevel;
    public player_script player;
    public string lightName;
    public ShadowBehavior shadow;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<player_script>();
        shadow = GameObject.Find("Shadow").GetComponent<ShadowBehavior>();

        //Finds the battery object and its component
        batteryManager = GameObject.Find("Battery").GetComponent<BatteryManager>();
        if (gameObject.CompareTag("Flashlight"))
        {
            lightName = "Flashlight";
        }
        if (gameObject.CompareTag("Candle"))
        {
            lightName = "Candle";
        }
        if (gameObject.CompareTag("Fireflies"))
        {
            lightName = "Fireflies";
        }
        player.currentLightSource = lightName;

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
            player.currentLightSource = "";

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow"))
        {
            if (lightName.Equals("Flashlight"))
            {
                shadow.GetComponent<ShadowBehavior>().telaportAway();
            }
            else
            {
                shadow.speed = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Shadow") && shadow.GetComponent<ShadowBehavior>().speed == 0)
        {
            shadow.speed = 3;
        }
    }
}
