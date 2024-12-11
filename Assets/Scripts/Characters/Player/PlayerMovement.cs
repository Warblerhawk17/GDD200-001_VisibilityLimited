using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // The speed of the player walking
    public GameObject sceneManager;
    public GameObject player;
    public float moveDuration = 0.5f;
    public GameObject dialogFind;
    public bool canMove;
    public float timeToArrow;
    public GameObject arrow;
    public Sprite charSprite;
    public Sprite findFriends;
    public GameObject charObj;
    public GameObject bubbleSprite;


    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 walkMovement; // Stores the direction of player movement
    private Animator anim;
    private player_script player_script;
    private float timeStill = 0;
    private float arrowAlpha = 0;
    private AudioSource walkingAudio;
    private Transform playerTrans;
    private float walkSpeed = 0.7f;
    private float runSpeed = 1.3f;

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

        arrow = GameObject.Find("FriendArrow");
        arrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        walkingAudio = GetComponent<AudioSource>();
        walkingAudio.mute = true;

        playerTrans = GetComponent<Transform>();
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
            timeStill = 0;
            arrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            arrowAlpha = 0;
            anim.SetBool("isWalking", true);
            walkingAudio.mute = false;

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
            timeStill += Time.deltaTime;
            walkingAudio.mute = true;

            if (timeStill > timeToArrow)
            {
                GameObject closestFriend = findClosestFriend();
                
                if (closestFriend != null)
                {
                    arrowAlpha += Time.deltaTime;
                    if (arrowAlpha > 1) 
                    { 
                        arrowAlpha = 1;
                    }
                    arrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, arrowAlpha);
                    Vector3 direction = closestFriend.transform.position - arrow.transform.position;
                    direction.Normalize();
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    arrow.transform.rotation = Quaternion.Euler(new Vector3(0,0,1) * angle - new Vector3(0,0,90));
                }
            }
        }
    }

    GameObject findClosestFriend()
    {
        GameObject closestFriend = null;
        GameObject[] friends = GameObject.FindGameObjectsWithTag("Friend");
        for (int i = 0; i < friends.Length; i++)
        {
            if (
                closestFriend != null &&
                !this.GetComponent<player_script>().friendList.Contains(friends[i]) &&
                Vector3.Distance(transform.position, friends[i].transform.position) < Vector3.Distance(transform.position, closestFriend.transform.position)
                )
            {
                closestFriend = friends[i];
            }
            if (closestFriend == null && !this.GetComponent<player_script>().friendList.Contains(friends[i]))
            {
                closestFriend = friends[i];
            }
        }
        return closestFriend;
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        if (canMove)
        {
            rb.velocity = 1.5f * speed * walkMovement;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.MovePosition(rb.position + 1.6f * Time.fixedDeltaTime * rb.velocity);
                anim.speed = runSpeed;
            }
            else
            {
                rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
                anim.speed = walkSpeed;
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            canMove = false;
            StartCoroutine(Cry());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Exit") && (playerTrans.position.y < 2.5f))
        {
            if (player_script.friendList.Count == 0)
            {
                canMove = false;
                StartCoroutine(MoveAndSpeak());
            }
        }
    }

    public IEnumerator MoveAndSpeak()
    {
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;
        Image charIm = charObj.GetComponent<Image>();
        Image bubbleIm = bubbleSprite.GetComponent<Image>();

        bubbleIm.sprite = findFriends;
        charIm.sprite = charSprite;


        charObj.SetActive(true);
        bubbleSprite.SetActive(true);
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
        yield return new WaitForSeconds(1.5f);
        charObj.SetActive(false);
        bubbleSprite.SetActive(false);
    }

    private IEnumerator Cry()
    {
        anim.Play("Wren_Crying");
        yield return new WaitForSeconds(1);
        canMove = true;
    }
}