using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    // Public variables
    public float speed = 5f;
    public float runSpeed = 8f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally
    public BatteryManager batteryManager;
    private int lives = 3;
    public int friendsSaved = 0;


    // Friend variable
    public List<GameObject> friendList = new List<GameObject>();


    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Check if diagonal movement is allowed
        if (canMoveDiagonally)
        {
            // Set movement direction based on input
            movement = new Vector2(horizontalInput, verticalInput);
        }
        else
        {
            // Determine the priority of movement based on input
            if (horizontalInput != 0)
            {
                isMovingHorizontally = true;
            }
            else if (verticalInput != 0)
            {
                isMovingHorizontally = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(runSpeed * Time.deltaTime * movement);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * movement);
        }



    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = movement * speed;
    }

    //If the player collides into a monster
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crawler"))
        {
            if (batteryManager.batteryCharge <= 0)
            {
                LoseLife();
            }
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
        else if (collision.gameObject.CompareTag("Shadow"))
        {
            if (batteryManager.batteryCharge <= 0) 
            {
                LoseLife();
            }
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
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
        lives--;
        transform.position = GameObject.Find("PlayerSpawn").transform.position;
        for (int i = 0; i < friendList.Count; i++) 
        {
            friendList[i].GetComponent<FriendFollow>().follow = null;
        }
        friendList.Clear();
        if (lives == 0)
        {
            Object.Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Friend") && !friendList.Contains(collision.gameObject))
        {
            Debug.Log("Collided with a friend");
            friendList.Add(collision.gameObject);
            friendList[friendList.Count - 1].GetComponent<FriendFollow>().follow = this.gameObject;
            friendList[friendList.Count - 1].GetComponent<FriendFollow>().followDistance = friendList.Count * 0.5f;



        }
        if (collision.gameObject.CompareTag("Exit"))
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
