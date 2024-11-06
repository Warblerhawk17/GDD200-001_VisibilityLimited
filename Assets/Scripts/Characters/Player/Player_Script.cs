using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed of the player walking
    public float runSpeed = 8f; // The speed at which the player runs

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    public BatteryManager batteryManager;
<<<<<<< Updated upstream
=======
    public LivesBehavior livesBehavior;
    public int lives = 3;
    public int friendsSaved = 0;
>>>>>>> Stashed changes
    public string currentLightSource;


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

        // Set movement direction based on input
        movement = new Vector2(horizontalInput, verticalInput);

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
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
        else if (collision.gameObject.CompareTag("Shadow"))
        {
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
<<<<<<< Updated upstream
=======
    private void LoseLife()
    {
        livesBehavior.LoseLife();
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


>>>>>>> Stashed changes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Friend") && !friendList.Contains(collision.gameObject) )
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
            }
        friendList.Clear();
    }


    }
}
