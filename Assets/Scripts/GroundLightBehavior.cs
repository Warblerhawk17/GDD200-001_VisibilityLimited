using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroundLightBehavior : MonoBehaviour
{
    float distance;
    float maxDistance = 1.0f;
    public GameObject player; // the player object
    public TextMeshProUGUI pickupText;
    LightSpawner gameManager;
    [SerializeField] bool isNearLight = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<LightSpawner>();
        pickupText = GameObject.Find("Pickup Text").GetComponentInChildren<TextMeshProUGUI>();
        pickupText.SetText("Press E to pickup" + " Flashlight");


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
            if (distance <= maxDistance && isNearLight == false) {
                isNearLight = true;
                pickupText.enabled = true;

            }
            if (Input.GetKeyDown(KeyCode.E) && (GameObject.FindWithTag("Flashlight") == false))
                
            {
                Destroy(this.gameObject);
                gameManager.lightSpawnRequested = true;

                pickupText.enabled = false;
            }


        }
        if (distance >= maxDistance && isNearLight == true) {
        pickupText.enabled = false;
        isNearLight = false;

        }
    }

    
}
