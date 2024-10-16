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

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Crawler")){
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
        else if (collision.gameObject.CompareTag("Shadow"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
        else if (collision.gameObject.CompareTag("Scream"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
    }
}
