using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed of the player walking
    public BatteryManager batteryManager;
    public LivesBehavior livesBehavior;
    public int lives = 3;
    public int friendsSaved = 0;
    public string currentLightSource;

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 walkMovement; // Stores the direction of player movement
    private Animator anim;

    // Friend variable
    public List<GameObject> friendList = new List<GameObject>();

    void Start()
    {
        // Initialize the Rigidbody2D component
        anim = GetComponent<Animator>(); //Animator object
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
        walkMovement = new Vector2(horizontalInput, verticalInput);

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            anim.SetBool("isWalking", true);
            if (horizontalInput > 0) //Walking right (D)
            {
                anim.SetBool("facingLeft", false);
                anim.SetBool("facingHoriz", true);
                anim.SetBool("facingUp", false);
                if (verticalInput < 0) //Walking down (S)
                {
                    anim.SetBool("facingUp", false);
                    anim.SetBool("facingHoriz", false);
                }
                else //walking up (W)
                {
                    anim.SetBool("facingUp", true);
                    anim.SetBool("facingHoriz", false);
                }
            }
            else //walking left (A)
            {
                anim.SetBool("facingLeft", true);
                anim.SetBool("facingHoriz", true);
                anim.SetBool("facingUp", false);
                if (verticalInput < 0) //Walking down (S)
                {
                    anim.SetBool("facingUp", false);
                    anim.SetBool("facingHoriz", false);
                }
                else //walking up (W)
                {
                    anim.SetBool("facingUp", true);
                    anim.SetBool("facingHoriz", false);
                }
            }
        }
        else anim.SetBool("isWalking", false);
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = walkMovement * speed * 1.5f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime * 2);
        }
        else
        {
            rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
        }
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
