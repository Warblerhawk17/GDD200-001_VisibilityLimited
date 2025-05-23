// Amelia Nehring
// GDD 200-001
// Fall 2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;
public class LightSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI pickupText; // Text prompting the user to press button.
    public GameObject groundFlashlightPrefab; // Prefab for the unequipped flashlight
    public GameObject groundCandlePrefab; // Prefab for the unequipped candle
    public GameObject groundFirefliesPrefab; // Prefab for the unequipped fireflies
    public GameObject equippedFlashlightPrefab; // Prefab of the equipped flashlight
    public GameObject equippedCandlePrefab; // Prefab of the equipped candle
    public GameObject equippedFirefliesPrefab; // Prefab of the equipped fireflies
    public bool lightSpawnRequested; // If a ground light is picked up, sends a signal to the GameManager to spawn a light
    public List<Vector2> spawnLocations; //Array that holds possible spawn locations
    public List<GameObject> spawnedLights; // List that holds all the spawned ground flashlights
    Vector2 chosenLocation; // Location that is randomly chosen for a light to spawn (from spawnLocations)
    [SerializeField] int numberToSpawn; // The total number of flashlights that should be spawned, should be <= to the number of spawn locations
    public BatteryManager batteryManager; // The battery manager script on the battery object
    player_script playerScript;
    public float chargeToSpawnWith;

    public GameObject player; // the player object
    public string lightToSpawn;
    [SerializeField] int minNumberOfEach;


    void Start()
    {
        SpawnStartingLights();
        playerScript = player.GetComponent<player_script>();

    }

    // Update is called once per frame
    void Update()
    {
        // Spawns a light if the groundLightBehavior sends a signal to do so
        if (lightSpawnRequested == true)
        {
            SpawnEquippedLight();
        }
    }
    // Spawns the equipped light variant 
    void SpawnEquippedLight()
    {
        if (lightToSpawn == "Flashlight")
        {
            GameObject newLight = (GameObject)Instantiate(equippedFlashlightPrefab, player.transform, worldPositionStays: false);
            if(chargeToSpawnWith != 0)
            {
                batteryManager.batteryCharge = chargeToSpawnWith;

            }
            else
            {
            batteryManager.batteryCharge = 50f;
            }
            chargeToSpawnWith = 0;
        }
        if (lightToSpawn == "Candle")
        {
            GameObject newLight = (GameObject)Instantiate(equippedCandlePrefab, player.transform, worldPositionStays: false);
            if (chargeToSpawnWith != 0)
            {
                batteryManager.batteryCharge = chargeToSpawnWith;

            }
            else
            {
                batteryManager.batteryCharge = 150f;
            }
            chargeToSpawnWith = 0;

        }
        if (lightToSpawn == "Fireflies")
        {
            GameObject newLight = (GameObject)Instantiate(equippedFirefliesPrefab, player.transform, worldPositionStays: false);
            if (chargeToSpawnWith != 0)
            {
                batteryManager.batteryCharge = chargeToSpawnWith;

            }
            else
            {
                batteryManager.batteryCharge = 100f;
            }
            chargeToSpawnWith = 0;

        }

        lightSpawnRequested = false;
    }

    public void spawnGroundUsedLight()
    {
        GameObject gameObjectToSpawn = groundFlashlightPrefab;
        if (playerScript.currentLightSource == "Candle")
        {
            gameObjectToSpawn = groundCandlePrefab;
        }
        if (playerScript.currentLightSource == "Fireflies")
        {
            gameObjectToSpawn = groundFirefliesPrefab;
        }
        GameObject newLight = (GameObject)Instantiate(gameObjectToSpawn, player.transform.position, Quaternion.identity);

        newLight.GetComponent<GroundLightBehavior>().storedCharge = batteryManager.batteryCharge;
    
}


    void SpawnStartingLights()
    {
        pickupText.enabled = false;
        minNumberOfEach = numberToSpawn / 3;
        int randomNumber;

        //Spawns each light at random locations

        for (int i = 0; i < minNumberOfEach; i++)
        {
            randomNumber = Random.Range(0, spawnLocations.Count);
            chosenLocation = spawnLocations[randomNumber];
            spawnedLights.Add(Instantiate(groundFlashlightPrefab, chosenLocation, Quaternion.identity));
            spawnLocations.RemoveAt(randomNumber);
        }

        for (int i = 0; i < minNumberOfEach; i++)
        {
            randomNumber = Random.Range(0, spawnLocations.Count);
            chosenLocation = spawnLocations[randomNumber];
            spawnedLights.Add(Instantiate(groundCandlePrefab, chosenLocation, Quaternion.identity));
            spawnLocations.RemoveAt(randomNumber);
        }

        for (int i = 0; i < minNumberOfEach; i++)
        {
            randomNumber = Random.Range(0, spawnLocations.Count);
            chosenLocation = spawnLocations[randomNumber];
            spawnedLights.Add(Instantiate(groundFirefliesPrefab, chosenLocation, Quaternion.identity));
            spawnLocations.RemoveAt(randomNumber);
        }

        if (spawnedLights.Count < numberToSpawn)
        {
            for (int i = 0; i < numberToSpawn - spawnedLights.Count; i++)
            {
                int randomNumber2 = Random.Range(1, 4);
                randomNumber = Random.Range(0, spawnLocations.Count);
                chosenLocation = spawnLocations[Random.Range(0, spawnLocations.Count - 1)];
                if (randomNumber2 == 1)
                {
                    spawnedLights.Add(Instantiate(groundFlashlightPrefab, chosenLocation, Quaternion.identity));
                }
                if (randomNumber2 == 2)
                {
                    spawnedLights.Add(Instantiate(groundCandlePrefab, chosenLocation, Quaternion.identity));
                }
                if (randomNumber2 == 3)
                {
                    spawnedLights.Add(Instantiate(groundFirefliesPrefab, chosenLocation, Quaternion.identity));
                }
                spawnLocations.RemoveAt(randomNumber);
            }
        }

    }

}
