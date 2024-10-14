//Made by Onyx
//TODO
//add condition for it to move directly towards the target if its very close and there is nothing between it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Node currentNode; //the current node it is at
    public List<Node> path; //the path of nodes it will travel
    public List<Node> patrolPath;
    public int patrolIndex = 0;
    public GameObject target; //the target, which it will go towards
    public float speed; //the speed of the npc
    public float directChaseDistance; //the distance before the npc will directly chase the player
    public LayerMask layerMask;


    void Start() //on start sets the currentNode to the closest node
    {
        currentNode = AStarManager.instance.FindNearestNode(transform.position);
    }

    void Update() 
    {

        //patroling stuff
        // Debug.Log("Error Spot 1");
        if (path.Count == 0)
        {
            //this is broken, it sets path to null sometimes, the problem is in AStarManager GeneratePath
            path = AStarManager.instance.GeneratePath(AStarManager.instance.FindNearestNode(transform.position), AStarManager.instance.FindNearestNode(patrolPath[patrolIndex].transform.position));
        }
        GoTowards(path[0].transform.position);
        //Debug.Log("Error Spot 2");
        if (Vector2.Distance(transform.position, path[0].transform.position) < 0.1f && path.Count != 0) //once it gets close enough to the node it removes it from the list
        {
            Debug.Log("Removed Node");
            currentNode = path[0];
            path.RemoveAt(0);
        }
        //Debug.Log("Error Spot 3");
        if (Vector2.Distance(transform.position, patrolPath[patrolIndex].transform.position) < 0.1f)
        {
            Debug.Log("Found Node");
            patrolIndex++;
            if (patrolIndex == patrolPath.Count)
            {
                patrolIndex = 0;
            }
        }
        //Debug.Log("Error Spot 4");





        //like some old raycast stuff
        //bool hasLineOfSight = false;
        /*
        RaycastHit2D ray = Physics2D.Raycast(transform.position, target.transform.position - transform.position);
        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
        Debug.Log(ray.collider);
        if (ray.collider == null)
        {
            Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.green);
        }*/




        /* some newer raycast stuff
        Vector2 direction = target.transform.position - transform.position; //direction the ray will point in
        //direction.Normalize();
        //Ray2D ray = new Ray2D(transform.position, direction); //Ray that will point towards the player
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, layerMask);
        if (hit.transform.gameObject.layer == target.layer)
        {
            Debug.DrawRay(transform.position, direction, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, direction, Color.red);
        }
        //Debug.Log(hit);

        //Debug.Log(hit.transform.gameObject.layer == target.layer);
        //Debug.Log(hit.transform.gameObject.layer);
        //Debug.Log(target.layer);
        if (Vector2.Distance(transform.position, target.transform.position) <= directChaseDistance && hit.transform.gameObject.layer == target.layer) //if the npc is close to the target and the path is unobstructed it will directly move towards them
        {
            GoTowards(target.transform.position);
            path.Clear();
            Debug.Log("Chasing Player");
        }
        */


        /*AStar stuff
        if (path.Count == 0) //if path is empty makes a new path
        {
            Debug.Log("Made New Path");
            path = AStarManager.instance.GeneratePath(AStarManager.instance.FindNearestNode(transform.position), AStarManager.instance.FindNearestNode(target.transform.position));
        }
        else
        {
             GoTowards(path[0].transform.position); //moves and rotates towards the first thing in path
             if (Vector2.Distance(transform.position, path[0].transform.position) < 0.1f) //once it gets close enough to the node it removes it from the list
             {
                 currentNode = path[0];
                 path.RemoveAt(0);
             }
            
        }*/
    }

    private void GoTowards(Vector2 goTo) //helper method which both moves and rotates the thing moving
    {
        transform.position = Vector2.MoveTowards(this.transform.position, goTo, speed * Time.deltaTime); //moves the npc
        Vector2 direction = goTo - (Vector2)transform.position; // finds direction between npc and target
        direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
        transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes npc rotation
    }

    private void OnDrawGizmos() //helper function to draw a line towards the node it is going towards
    {
        Gizmos.color = Color.blue;
        if (path.Count != 0)
        {
            Gizmos.DrawLine(transform.position, path[0].transform.position);
        }
       
    }
}
