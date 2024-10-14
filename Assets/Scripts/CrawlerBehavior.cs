using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBehavior : MonoBehaviour
{

    public Node currentNode; //the current node it is at
    public List<Node> path; //the path of nodes it will travel
    public List<Node> patrolPath; //the nodes that will be patroled by the crawler
    public GameObject target; //the target, which it will go towards
    public float speed; //the speed of the npc
    public float visionRadius; //the radius the crawler can see
    private float sawPlayerCount; //used to make the monster pause for a second after loosing sight of the player


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

        //chase the player
        if (Vector2.Distance(transform.position, target.transform.position) <= visionRadius)
        {

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
}
