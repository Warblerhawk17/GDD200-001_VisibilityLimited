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
    public GameObject player; // the player object
    [SerializeField] int numberToSpawn; // The total number of flashlights that should be spawned
    public GameObject equippedFlashlightPrefab; // Prefab of the equipped flashlight
    public BatteryManager batteryManager; // The battery manager script on the battery object
    float distance; //
    float maxDistance; //Maximum distance, where a player below the maxDistance will be prompted
    GameObject currentEquippedLight; // (Unused) the light the player currently has equipped.
    float flashlightCharge;
        // For if the player picks up a new light while holding a light
        // that still has charge.

    void Start()
    {
        pickupText.enabled = false;
        for (int i = 0; i < numberToSpawn; i++)
        {
            int randomNumber = Random.Range(0, spawnLocations.Count);
            chosenLocation = spawnLocations[randomNumber];
            spawnedLights.Add(Instantiate(groundFlashlightPrefab, chosenLocation, Quaternion.identity));
            spawnLocations.RemoveAt(randomNumber); // So that multiple lights dont spawn in the same position
        }
    }

    // Update is called once per frame
    void Update()
    {
        PromptPickup();
    }

    void PromptPickup()
    {
        if (spawnedLights.Count > 0)
        {
            for (int i = 0; i < spawnedLights.Count; i++)
            {
                if (spawnedLights[i] != null)
                {
                    distance = Vector2.Distance(player.transform.position, spawnedLights[i].transform.position);
                    maxDistance = 3.0f;
                }
                if (distance <= maxDistance)
                {
                    pickupText.enabled = true;
                    pickupText.SetText("Press E to pickup" + " Flashlight");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                       /* if(player.transform.childCount > 0)
                        {
                            currentEquippedLight = GameObject.Find("Flashlight(Clone)");
                            Instantiate(groundFlashlightPrefab, spawnedLights[i].transform);
                            Destroy(currentEquippedLight);
                        }
                       */ // Code for if player already has a light equipped.
                        pickupText.enabled = false;
                        Destroy(spawnedLights[i]);
                        Instantiate(equippedFlashlightPrefab, player.transform, worldPositionStays: false);
                        batteryManager.batteryCharge = 100f;
                    }
                }
                else if (distance > maxDistance)
                {
                    pickupText.enabled = false;
                }

            }
        }
        
    }

}
