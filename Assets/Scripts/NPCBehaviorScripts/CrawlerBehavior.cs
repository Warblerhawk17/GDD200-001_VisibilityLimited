using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBehavior : MonoBehaviour
{

    private Node currentNode; //the current node it is at
    private List<Node> path = new List<Node>(); //the path of nodes it will travel
    public List<Node> patrolPath; //the nodes that will be patroled by the crawler
    private int patrolIndex; //the index in patrol path the crawler is headed towards
    public GameObject target; //the target, which it will go towards
    public float speed; //the speed of the crawler
    public float visionRadius; //the radius the crawler can see
    public LayerMask layerMask;
    private float stopCount; //used to make the monster pause for a second after loosing sight of the player
    public float defaultStopCount; //default value that sawPlayerCount is set to
    public float hitStopCount; //how long it stops when it hits the player
    public bool isLookingAt; //checks if the crawler is looking at it


    // Start is called before the first frame update
    void Start()
    {
        currentNode = AStarManager.instance.FindNearestNode(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //behavior of the crawler
        //if the player is within its search radius and within line of sight it will move towards it
        //if it was chasing the player but lost sight or they are out of their radius it will stand still for a few seconds
        //if neither of the above appply it will move to the next point in its patrol path

        //todo
        //the bug is that as soon as it sees the player it also stops 


        //pause behavior
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, float.MaxValue, layerMask);
        
        if (!(hit.collider.gameObject.layer == target.gameObject.layer && Vector2.Distance(transform.position, target.transform.position) <= visionRadius))
        {
            isLookingAt = false;
        }

        if (stopCount > 0f && !isLookingAt)
        {
            Debug.Log("Stopped");
            stopCount -= Time.deltaTime;
        }
        //chase the player
        //a raycast is done from the monster to the player only interacts with things on layerMask (set to player and wall layers)
        //checks if the layer of the raycast and target are the same and if the target is within the vision radius
        else if (hit.collider.gameObject.layer == target.gameObject.layer && Vector2.Distance(transform.position,target.transform.position) <= visionRadius) 
        {
            Debug.Log("Chase");
            GoTowards(target.transform.position); //moves towards target
            stopCount = defaultStopCount;
            isLookingAt = true;
        }
        // Patroling behavior
        else
        {
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
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == target.layer)
            {
            stopCount = hitStopCount;
            }
    }


    private void GoTowards(Vector2 goTo) //helper method which both moves and rotates the thing moving
    {
        transform.position = Vector2.MoveTowards(this.transform.position, goTo, speed * Time.deltaTime); //moves the npc
        Vector2 direction = goTo - (Vector2)transform.position; // finds direction between npc and target
        direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
        transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes npc rotation
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
