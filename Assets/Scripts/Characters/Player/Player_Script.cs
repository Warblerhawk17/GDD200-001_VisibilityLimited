using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public string currentLightSource;


    // Private variables 


    // Friend variable
    public List<GameObject> friendList = new List<GameObject>();

    void Start()
    {
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
        transform.position = GameObject.Find("PlayerSpawn").transform.position;
        for (int i = 0; i < friendList.Count; i++)
        {
            friendList[i].GetComponent<FriendFollow>().followTarget = null;
        }
        friendList.Clear();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("Friend") && !friendList.Contains(collision.gameObject) )
        {
            Debug.Log("Collided with a friend");
            friendList.Add(collision.gameObject);
            friendList[friendList.Count - 1].GetComponent<FriendFollow>().follow = this.gameObject;
            friendList[friendList.Count - 1].GetComponent<FriendFollow>().followDistance = friendList.Count * 0.5f;

        }*/
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (friendList != null)
            {
                for (int i = 0; i < friendList.Count; i++)
                {
                    Object.Destroy(friendList[i]);
                    friendsSaved++;
                    MonsterSpawner.instance.SpawnMonsters(friendsSaved - 1);
                }
                friendList.Clear();
            }
        }
    }

    private IEnumerator CallFlicker()
    {
        Light2D light1 = null;
        Debug.Log("Flicker called");

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
