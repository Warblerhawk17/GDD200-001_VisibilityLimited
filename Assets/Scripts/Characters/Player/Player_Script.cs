using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    // Public variables
    public float speed = 5f;
    public float runSpeed = 8f; // The speed at which the player moves
    public bool isFacingUp = false;
    public bool isFacingLeft = false;

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool isMovingHorizontally = true; // Flag to track if the player is moving horizontally
    public BatteryManager batteryManager;
    private Animator anim;

    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalInput, verticalInput);

        // Determine the priority of movement based on input
        if (Mathf.Abs(horizontalInput) != 0 || Mathf.Abs(verticalInput) != 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("movingUp", verticalInput);
            anim.SetFloat("movingLeft", horizontalInput);
        }
        else 
        {
            anim.SetBool("isWalking", false);
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
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
        // disabling temporarily as these monsters will not be present in Alpha
     /*   
        else if (collision.gameObject.CompareTag("Shadow"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
        }
        else if (collision.gameObject.CompareTag("Scream"))
        {
            batteryManager.batteryCharge = batteryManager.batteryCharge - 10;
       }
     */
    }
}
