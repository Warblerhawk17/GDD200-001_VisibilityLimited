using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed of the player walking
    public GameObject sceneManager;
    public GameObject player;
    public float moveDuration = 0.5f;

    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 walkMovement; // Stores the direction of player movement
    private Animator anim;
    private SceneMan sceneMan;
    private player_script player_script;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>(); //Animator object

        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        sceneManager = GameObject.Find("SceneManager");
        sceneMan = sceneManager.GetComponent<SceneMan>();
        player_script = player.GetComponent<player_script>();

        canMove = !sceneMan.isGamePaused;
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Set movement direction based on input
        walkMovement = new Vector2(horizontalInput, verticalInput);

        if ((Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0) && canMove)
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
                //Debug.Log("facingLeft");
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MoveAndSpeak());
        }

    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        if (canMove)
        {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (player_script.friendList.Count == 0)
            {
                canMove = false;
                StartCoroutine(MoveAndSpeak());
            }
        }
    }


private IEnumerator MoveAndSpeak()
    {
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;

        if (anim != null)
        {
            anim.Play("Wren_W_Walk");
        }

        while (elapsedTime < moveDuration)
        {
            // Linearly interpolate the position upwards
            transform.position = startPosition + Vector2.up * speed * elapsedTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        anim.Play("Jolene_W_Idle");
        canMove = true;
    }
}
