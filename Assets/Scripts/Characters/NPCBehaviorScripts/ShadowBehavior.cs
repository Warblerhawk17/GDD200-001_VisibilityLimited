using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ShadowBehavior : MonoBehaviour
{
    private Node currentNode; //the current node it is at
    private List<Node> path = new List<Node>(); //the path of nodes it will travel
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public GameObject target; //the target, which it will go towards
    public float speed; //the speed of the crawler
    public LayerMask layerMask;
    private float chaseRadius = 5;
    private float timeChased;

    private GameObject frontEyes;
    private GameObject lEyes;
    private GameObject rEyes;

    // Start is called before the first frame update
    void Start()
    {
        currentNode = AStarManager.instance.FindNearestNode(transform.position);
        spriteRenderer = GetComponent<SpriteRenderer>(); //Sprite Renderer object
        anim = GetComponent<Animator>(); //Animator object

        frontEyes = GameObject.Find("Shadow").transform.GetChild(0).gameObject;
        lEyes = GameObject.Find("Shadow").transform.GetChild(1).gameObject;
        rEyes = GameObject.Find("Shadow").transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Shadow behavior
        //move towards the player using pathfinding
        //once close to the player move directly towards them
        //dim light pushes it back, flashlight banishes it to the furthest node, will also telaport away after directly chansing for a time

        //move directly towards
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, float.MaxValue, layerMask);
        if (hit.collider.gameObject.layer == target.gameObject.layer && 
            Vector2.Distance(transform.position, target.transform.position) <= chaseRadius)
        {
            //Debug.Log("Chase");
            GoTowards(target.transform.position); //moves towards target
            timeChased += Time.deltaTime;
            if (timeChased > 5)
            {
                StartCoroutine( telaportAway());
                timeChased = 0;
            }
        }
        //pathfind towards 
        else
        {
            //Debug.Log("Search");
            if (path.Count == 0 || Vector2.Distance(path[path.Count-1].transform.position,target.transform.position) > 5) { //make new path
                Node nearestNode = AStarManager.instance.FindNearestNode(transform.position); //the node nearest to the crawler
                Node targetNode = AStarManager.instance.FindNearestNode(target.transform.position);
                path = AStarManager.instance.GeneratePath(nearestNode, targetNode); //makes a path from the crawler to the next node in the patrol path
            }
            GoTowards(path[0].transform.position); //goes towards the next node in path
            if (Vector2.Distance(transform.position, path[0].transform.position) < 0.1f) //removes node if it gets too close to it
            {
                currentNode = path[0];
                path.RemoveAt(0);
            }
        }
    }   

    public IEnumerator telaportAway()
    {
        Debug.Log("Teleported");
        float storedSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(1);
        transform.position = AStarManager.instance.FindFurthestNode(transform.position).transform.position;
        speed = storedSpeed;
        anim.SetBool("isAttacking", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == target.layer)
        {
            Debug.Log("Hit player");
            anim.SetBool("isAttacking", true);
            StartCoroutine(telaportAway());
        }
    }

    private void GoTowards(Vector2 goTo) //helper method which both moves and rotates the thing moving
    {
        transform.position = Vector2.MoveTowards(this.transform.position, goTo, speed * Time.deltaTime); //moves the npc
        Vector2 direction = goTo - (Vector2)transform.position; // finds direction between npc and target
        direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
        
        if (45 < angle && angle <= 135) //facing up (W)
        {
            anim.SetBool("facingUp", true);
            anim.SetBool("facingHorizontal", false);
            frontEyes.SetActive(false);
            lEyes.SetActive(false);
            rEyes.SetActive(false);
        }
        else if (-135 > angle || angle > 135) //facing left (A)
        {
            anim.SetBool("facingUp", false);
            anim.SetBool("facingHorizontal", true);
            spriteRenderer.flipX = false;
            frontEyes.SetActive(false);
            lEyes.SetActive(true);
            rEyes.SetActive(false);
        }
        else if (-45 > angle && angle >= -135) //facing down (S)
        {
            anim.SetBool("facingUp", false);
            anim.SetBool("facingHorizontal", false);
            frontEyes.SetActive(true);
            lEyes.SetActive(false);
            rEyes.SetActive(false);
        }
        else //facing right (D)
        {
            anim.SetBool("facingUp", false);
            anim.SetBool("facingHorizontal", true);
            spriteRenderer.flipX = true; //flips sideview sprite to look the correct way
            frontEyes.SetActive(false);
            lEyes.SetActive(false);
            rEyes.SetActive(true);
        }
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes npc rotation
    }
}
