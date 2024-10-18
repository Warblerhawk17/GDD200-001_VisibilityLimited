using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class LightInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI pickupText; // Text prompting the user to press button.
    public GameObject groundFlashlightPrefab; // Prefab for the unequipped flashlight
    public List<Vector2> spawnLocations; //Array that holds possible spawn locations
    public List<GameObject> spawnedLights; // List that holds all the spawned ground flashlights
    Vector2 chosenLocation; // Location that is randomly chosen for a light to spawn (from spawnLocations)
    [SerializeField] int numberToSpawn; // The total number of flashlights that should be spawned
    public GameObject equippedFlashlightPrefab; // Prefab of the equipped flashlight
    public BatteryManager batteryManager; // The battery manager script on the battery object
    float distance; //
    float maxDistance; //Maximum distance, where a player below the maxDistance will be prompted
    GameObject currentEquippedLight; // (Unused) the light the player currently has equipped.
    float flashlightCharge;
    // For if the player picks up a new light while holding a light
    // that still has charge.
    public bool lightSpawnRequested;
    public GameObject player; // the player object


    void Start()
    {
        pickupText.enabled = false;
        for (int i = 0; i < numberToSpawn; i++)
        {
            int randomNumber = Random.Range(0, spawnLocations.Count - 1);
            
            chosenLocation = spawnLocations[randomNumber];
            spawnedLights.Add(Instantiate(groundFlashlightPrefab, chosenLocation, Quaternion.identity));
            
            spawnLocations.RemoveAt(randomNumber); // So that multiple lights dont spawn in the same position

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lightSpawnRequested == true)
        {
            SpawnEquippedLight();
        }
    }

    void SpawnEquippedLight()
    {

        Instantiate(equippedFlashlightPrefab, player.transform, worldPositionStays: false);
        batteryManager.batteryCharge = 100f;
        lightSpawnRequested = false;
    }

}
