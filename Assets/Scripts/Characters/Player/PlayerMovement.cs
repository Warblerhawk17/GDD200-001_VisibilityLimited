using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed of the player walking
    public GameObject sceneManager;
    public GameObject player;
    public float moveDuration = 0.5f;
    public GameObject dialogFind;
    public bool canMove;


    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 walkMovement; // Stores the direction of player movement
    private Animator anim;
    private player_script player_script;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>(); //Animator object

        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        sceneManager = GameObject.Find("SceneManager");
        player_script = player.GetComponent<player_script>();
        canMove = true;
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
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        if (canMove)
        {
            rb.velocity = 1.5f * speed * walkMovement;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.MovePosition(rb.position + 2 * Time.fixedDeltaTime * rb.velocity);
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

        dialogFind.SetActive(true);
        anim.Play("Wren_W_Walk");
        while (elapsedTime < moveDuration)
        {
            // Linearly interpolate the position upwards
            transform.position = startPosition + elapsedTime * speed * Vector2.up;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        anim.Play("Wren_W_Idle");
        canMove = true;
        yield return new WaitForSeconds(1);
        dialogFind.SetActive(false);
    }
}
