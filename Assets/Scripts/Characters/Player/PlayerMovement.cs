using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed of the player walking

    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 walkMovement; // Stores the direction of player movement
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Animator object

        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
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
                //Debug.Log("facingRight");
                anim.SetBool("facingHoriz", true);
                anim.SetBool("facingLeft", false);
                anim.SetBool("facingUp", false);
            }
            else if (horizontalInput < 0) //walking left (A)
            {
                anim.SetBool("facingHoriz", true);
                anim.SetBool("facingLeft", true);
                anim.SetBool("facingUp", false);
               // Debug.Log("facingLeft");
            }
            else if (verticalInput < 0) //Walking down (S)
            {
                anim.SetBool("facingHoriz", false);
                //Debug.Log("facingDown");
                anim.SetBool("facingLeft", false);
                anim.SetBool("facingUp", false);
            }
            else if (verticalInput > 0) //walking up (W)
            {
                anim.SetBool("facingHoriz", false);
                //Debug.Log("facingUp");
                anim.SetBool("facingLeft", false);
                anim.SetBool("facingUp", true);
            }

            //switch (horizontalInput, verticalInput)
            //{
            //    case horizontalInput > 0:
            //        anim.SetBool("facingLeft", false);
            //        anim.SetBool("facingUp", false);
            //        anim.SetBool("facingHoriz", true);
            //        break;
            //    case horizontalInput < 0:
            //        anim.SetBool("facingLeft", true);
            //        anim.SetBool("facingUp", false);
            //        anim.SetBool("facingHoriz", true);
            //        break;
            //    case verticalInput < 0:
            //        anim.SetBool("facingLeft", false);
            //        anim.SetBool("facingUp", false);
            //        anim.SetBool("facingHoriz", false);
            //        break;
            //    case verticalInput > 0:
            //        anim.SetBool("facingLeft", false);
            //        anim.SetBool("facingUp", true);
            //        anim.SetBool("facingHoriz", false);
            //        break;
            //    default:
            //        Debug.Log("Uhhh not moving?");
            //        break;
            //}
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
}
