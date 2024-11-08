using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBehavior : MonoBehaviour
{

    private Node currentNode; //the current node it is at
    private List<Node> path = new List<Node>(); //the path of nodes it will travel
    public List<Node> patrolPath; //the nodes that will be patroled by the crawler
    private int patrolIndex; //the index in patrol path the crawler is headed towards
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float angle;

    public GameObject target; //the target, which it will go towards
    public float speed; //the speed of the crawler
    public float visionRadius; //the radius the crawler can see
    public LayerMask layerMask;
    private float stopCount; //used to make the monster pause for a second after loosing sight of the player
    public float defaultStopCount; //default value that sawPlayerCount is set to
    public float hitStopCount; //how long it stops when it hits the player
    public float defaultHitStopCount; //default value that hitStopCount is set to
    public bool isLookingAt; //checks if the crawler is looking at it
    


    // Start is called before the first frame update
    void Start()
    {
        currentNode = AStarManager.instance.FindNearestNode(transform.position);
        spriteRenderer = GetComponent<SpriteRenderer>(); //Sprite Renderer object
        anim = GetComponent<Animator>(); //Animator object
        defaultHitStopCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        visionRadius = 3 + target.GetComponent<Player_Script>().friendList.Count;
        if (target.GetComponent<Player_Script>().currentLightSource == "Flashlight")
        {
            visionRadius += 4;
        }
        else if (target.GetComponent<Player_Script>().currentLightSource == "Candle")
        {
            visionRadius += 2;
        }
        //New behavior for the crawler
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, float.MaxValue, layerMask);
        

        // pause if it hit the player
        if (hitStopCount > 0f)
        {
            Debug.Log("Counting");
            hitStopCount -= Time.deltaTime;
            if (hitStopCount == 1)
            {
                anim.SetBool("isAttacking", false);
            }
        }

        // move towards the player if within radius and no walls blocking it
        else if (hit && hit.collider.gameObject.layer == target.gameObject.layer && Vector2.Distance(transform.position, target.transform.position) <= visionRadius)
        {
            Debug.Log("Chase");
            anim.SetBool("isAttacking", false);
            GoTowards(target.transform.position); //moves towards target
            stopCount = defaultStopCount;
        }

        // pause if the player just left line of sight
        else if (stopCount > 0f)
        {
            anim.SetBool("isAttacking", false);
            //Debug.Log("Stopped");
            stopCount -= Time.deltaTime;
        }

        // travel along its patrol route
        else
        {
            anim.SetBool("isAttacking", false);
            Debug.Log("Patrol");
            //Debug.Log(path);
            if (path.Count == 0)
            {
                Node nearestNode = AStarManager.instance.FindNearestNode(transform.position); //the node nearest to the crawler
                path = AStarManager.instance.GeneratePath(nearestNode, patrolPath[patrolIndex]); //makes a path from the crawler to the next node in the patrol path
            }
            GoTowards(path[0].transform.position); //goes towards the next node in path
            if (Vector2.Distance(transform.position, path[0].transform.position) < 0.1f) //removes node if it gets too close to it
            {
                currentNode = path[0];
                path.RemoveAt(0);
            }
            if (Vector2.Distance(transform.position, patrolPath[patrolIndex].transform.position) < 0.1f) //moves to next index in its patrol path if it reaches it
            {
                patrolIndex++;
                if (patrolIndex == patrolPath.Count) //loops around if it reaches the end
                {
                    patrolIndex = 0;
                }
            }
        }


    }

    //add some sort of collider with the player, stopCount = hitStopCount
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == target.layer)
        {
            Debug.Log("Play attack anim");
            anim.SetBool("isAttacking", true);
            hitStopCount = defaultHitStopCount;
        }
    }


    private void GoTowards(Vector2 goTo) //helper method which both moves and rotates the thing moving
    {
        anim.SetBool("isAttacking", false);
        transform.position = Vector2.MoveTowards(this.transform.position, goTo, speed * Time.deltaTime); //moves the npc
        Vector2 direction = goTo - (Vector2)transform.position; // finds direction between npc and target
        direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
        
        //Debug.Log(angle);
        if (45 < angle && angle <= 135) //facing up (W)
        {
            anim.SetBool("facingUp", true);
            anim.SetBool("facingHorizontal", false);
        }
        else if (-135 > angle || angle > 135) //facing left (A)
        {
            anim.SetBool("facingUp", false);
            anim.SetBool("facingHorizontal", true);
            spriteRenderer.flipX = false;
        }
        else if (-45 > angle && angle >= -135) //facing down (S)
        {
            anim.SetBool("facingUp", false);
            anim.SetBool("facingHorizontal", false);
        }
        else //facing right (D)
        {
            anim.SetBool("facingUp", false);
            anim.SetBool("facingHorizontal", true);
            spriteRenderer.flipX = true; //flips the sideview sprite to look the correct way
        }
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes npc rotation
        
    }

    /*private void OnDrawGizmos() //helper function to draw a line towards the node it is going towards
    {
        Gizmos.color = Color.blue;
        if (path.Count != 0)
        {
            Gizmos.DrawLine(transform.position, path[0].transform.position);
        }

    }*/
}
