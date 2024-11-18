// Amelia Nehring
// GDD 200-001
// Fall 2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroundLightBehavior : MonoBehaviour
{
    float distance;
    float maxDistance = 1.0f;
    public GameObject player; // the player object
    public player_script playerScript;
    public TextMeshProUGUI pickupText;
    LightSpawner gameManager;
    [SerializeField] bool isNearLight = false;
    string lightType;
    string lightTypeRequested;
    public float storedCharge;
    BatteryManager batteryManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<LightSpawner>();
        pickupText = GameObject.Find("Pickup Text").GetComponentInChildren<TextMeshProUGUI>();
        playerScript = player.GetComponent<player_script>();
        batteryManager = GameObject.Find("Battery").GetComponent<BatteryManager>();

    }

    // Update is called once per frame
    void Update()
    {
        


        PromptUser();
    }

    void PromptUser()
    {

        distance = Vector2.Distance(player.transform.position, this.gameObject.transform.position);
        if (distance <= maxDistance)
        {
            if(this.tag == "Ground Flashlight")
            {
                lightType = "Flashlight";
            }
            else if (this.tag ==  "Ground Candle")
            {
                lightType = "Candle";

            }
            else if (this.tag == "Ground Fireflies")
            {
                lightType = "Fireflies";

            }
            pickupText.SetText("Press E to pickup " + lightType);
            if (distance <= maxDistance && isNearLight == false) {
                isNearLight = true;
                pickupText.enabled = true;

            }
            if (Input.GetKeyDown(KeyCode.E)) // Pick up light
                
            {
                if(playerScript.currentLightSource != "") // If player already has a light source
                {
                    gameManager.spawnGroundUsedLight();
                    Destroy(GameObject.FindWithTag(playerScript.currentLightSource));
                    playerScript.currentLightSource = "";
                }
                if(this.storedCharge != 0) // If the light has been previosuly used and has a stored charge
                {
                    gameManager.chargeToSpawnWith = storedCharge;
                }
                Destroy(this.gameObject);
                gameManager.lightSpawnRequested = true;
                gameManager.lightToSpawn = lightType;
                pickupText.enabled = false;
            }


        }
        if (distance >= maxDistance && isNearLight == true) {
        pickupText.enabled = false;
        isNearLight = false;

        }
    }

    
}
