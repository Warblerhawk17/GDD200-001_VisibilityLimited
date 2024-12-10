using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class player_script : MonoBehaviour
{
    // Public variables
    public BatteryManager batteryManager;
    public LivesBehavior livesBehavior;
    public int lives = 3;
    public int friendsSaved = 0;
    public int friendsPickedUp = 0;
    public string currentLightSource;
    public int rotateSpeed = 10;
    public Sprite wrenSprite;
    public Transform player;


    // Private variables 
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;


    // Friend variable
    public List<GameObject> friendList = new List<GameObject>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    //If the player collides into a monster
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crawler"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
            StartCoroutine(CallFlicker());
            if (currentLightSource == "")
            {
                LoseLife();
            }
        }
        else if (collision.gameObject.CompareTag("Shadow"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
            StartCoroutine(CallFlicker());
            if (currentLightSource == "")
            {
                LoseLife();
            }
        }
        
        // disabling temporarily as this monster will not be present in Beta  
        /*
           else if (collision.gameObject.CompareTag("Scream"))
           {
               batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
          }
        */
    }

    public void LoseLife()
    {
        //Debug.Log("LoseLife called");
        livesBehavior.LoseLife();
        StartCoroutine(DamageFlash.instance.flash());
        lives--;
        playerMovement.canMove = false;
        StartCoroutine(Respawn());
        for (int i = 0; i < friendList.Count; i++)
        {
            friendList[i].GetComponent<FriendFollow>().followTarget = null;
        }
        friendList.Clear();
    }

    private IEnumerator Respawn()
    {
        int flashes = 3;
        float flashInterval = 0.2f;

        for (int i = 0; i < flashes; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(flashInterval);
        }
        spriteRenderer.enabled = true; // Ensure it's visible at the end

        // Respawn sprite by moving it to a new position
        transform.position = GameObject.Find("PlayerSpawn").transform.position;

        // Blink effect after respawn
        for (int i = 0; i < flashes; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(flashInterval);
        }
        spriteRenderer.enabled = true; // Ensure it's visible at the end

        // Reset rotation to the default state
        spriteRenderer.transform.localRotation = Quaternion.identity;
        playerMovement.canMove = true;
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (player.position.y < 2.5f)
            {
                if (friendList != null)
                {
                    for (int i = 0; i < friendList.Count; i++)
                    {
                        Object.Destroy(friendList[i]);
                        friendsSaved++;
                        TMPro.TextMeshProUGUI counter = GameObject.Find("FriendCounter").GetComponent<TMPro.TextMeshProUGUI>();
                        counter.text = friendsSaved + " / 4 Friends Saved";
                    }
                    friendList.Clear();
                }
            }
        }
    }

    private IEnumerator CallFlicker()
    {
        Light2D light1 = null;

        if (GameObject.FindWithTag("Flashlight"))
        {
            light1 = GameObject.Find("Flashlight").GetComponent<Light2D>();
        }
        else if (GameObject.FindWithTag("Candle"))
        {
            light1 = GameObject.Find("CandleRotation").GetComponent<Light2D>();
        }
        else if (GameObject.FindWithTag("Fireflies"))
        {
            light1 = GameObject.Find("FirefliesRotate").GetComponent<Light2D>();
        }

        if (light1 != null)
        {
            for (int i = 0; i < 6; i++)
            {
                light1.enabled = !light1.enabled;
                yield return new WaitForSeconds(Random.Range(0f, 0.3f));
            }
        }
    }
}
