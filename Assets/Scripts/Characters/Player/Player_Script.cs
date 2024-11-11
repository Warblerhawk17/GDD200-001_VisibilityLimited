using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
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
            if (currentLightSource == "" )
            {
                LoseLife();
            }
        }
        else if (collision.gameObject.CompareTag("Shadow"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
            if (currentLightSource == "")
            {
                LoseLife();
            }
        }
        else if (collision.gameObject.CompareTag("Friend"))
        {
            Debug.Log("Collided with a friend");
            friendList.Add(collision.gameObject);
            if (friendList.Count == 1)
            {
                friendList[0].GetComponent<FriendFollow>().follow = this.gameObject;
            }
            else
            {
                friendList[friendList.Count - 1].GetComponent<FriendFollow>().follow = friendList[friendList.Count - 2].gameObject;
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

    private void LoseLife()
    {
        Debug.Log("LoseLife called");
        livesBehavior.LoseLife();
        lives--;
        transform.position = GameObject.Find("PlayerSpawn").transform.position;
        for (int i = 0; i < friendList.Count; i++) 
        {
            friendList[i].GetComponent<FriendFollow>().follow = null;
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
            for (int i = 0; i < friendList.Count; i++)
            { 
                Object.Destroy(friendList[i]);
                friendsSaved++;
                MonsterSpawner.instance.SpawnMonsters(friendsSaved-1);
            }
        friendList.Clear();
        }
    }
}
