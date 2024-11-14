using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FriendFollow : MonoBehaviour
{
    public GameObject follow;
    public float followDistance;
    public LayerMask layerMask;

    private Node currentNode; //the current node it is at
    private List<Node> path = new List<Node>(); //the path of nodes it will travel
    private float speed = 10;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Animator object
    }

    // Update is called once per frame
    void Update()
    {
        if (follow != null)
        {
            //Vector2 rayCastStart = transform.position + Vector3.Normalize(follow.transform.position - transform.position)*1.5f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.Normalize(follow.transform.position - transform.position), float.MaxValue, layerMask);
            //Debug.DrawRay(transform.position, Vector3.Normalize(follow.transform.position - transform.position), Color.red);
            //Debug.Log(hit.collider.gameObject == gameObject);
            //Debug.Log("Is self collide " + (hit.collider.gameObject == follow.gameObject) + " rayCastStart " + (rayCastStart) + " direction " + (Vector3.Normalize(follow.transform.position - transform.position)) + " Follow " + (follow.transform.position) + " my position" + (transform.position));
            if (hit.collider.gameObject != follow.gameObject)
            {
                //Debug.Log("Non Direct Move");
                if (path.Count == 0)
                { //make new path
                    Node nearestNode = AStarManager.instance.FindNearestNode(transform.position); //the node nearest to the friend
                    Node targetNode = AStarManager.instance.FindNearestNode(follow.transform.position);
                    path = AStarManager.instance.GeneratePath(nearestNode, targetNode); //makes a path from the crawler to the next node in the patrol path
                }
                GoTowards(path[0].transform.position); //goes towards the next node in path
                //Debug.DrawRay(transform.position, path[0].transform.position - transform.position, Color.red);
                if (Vector2.Distance(transform.position, path[0].transform.position) < 0.1f) //removes node if it gets too close to it
                {
                    currentNode = path[0];
                    path.RemoveAt(0);
                }
            }
            else if (Vector2.Distance(transform.position, follow.transform.position) > followDistance)
            {
                //Debug.Log("Direct Move");
                GoTowards(follow.transform.position);
                path.Clear();
            }
        }
        else anim.SetBool("isWalking", false);
    }

    private void GoTowards(Vector2 goTo) //helper method which both moves and rotates the thing moving
    {
        transform.position = Vector2.MoveTowards(this.transform.position, goTo, speed * Time.deltaTime); //moves the npc
        Vector2 direction = goTo - (Vector2)transform.position; // finds direction between npc and target
        direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
        Debug.Log(angle);
        anim.SetBool("isWalking", true);
        if (45 < angle && angle <= 135) //facing up (W)
        {
            Debug.Log("facing Up");
            anim.SetBool("facingHoriz", false);
            anim.SetBool("facingUp", true);
            anim.SetBool("facingLeft", false);
        }
        else if (-135 > angle || angle > 135) //facing left (A)
        {
            Debug.Log("facing Left");
            anim.SetBool("facingHoriz", true);
            anim.SetBool("facingUp", false);
            anim.SetBool("facingLeft", true);
        }
        else if (-45 > angle && angle >= -135) //facing down (S)
        {
            Debug.Log("facing Down");
            anim.SetBool("facingHoriz", false);
            anim.SetBool("facingUp", false);
            anim.SetBool("facingLeft", false);
        }
        else //facing right (D)
        {
            Debug.Log("facing Right");
            anim.SetBool("facingHoriz", true);
            anim.SetBool("facingUp", false);
            anim.SetBool("facingLeft", false);
        }
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes npc rotation
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.GetComponent<Player_Script>().friendList.Contains(this.gameObject))
        {
            Player_Script player = collision.GetComponent<Player_Script>();
            player.friendList.Add(this.gameObject);
            follow = player.gameObject;
            followDistance = player.friendList.Count * 0.5f;
        }

        /*!collision.GetComponent<Player_Script>.friendList.Contains(this)
          if (collision.gameObject.CompareTag("Friend") && !friendList.Contains(collision.gameObject))
        {
            Debug.Log("Collided with a friend");
            friendList.Add(collision.gameObject);
            friendList[friendList.Count - 1].GetComponent<FriendFollow>().follow = this.gameObject;
            friendList[friendList.Count - 1].GetComponent<FriendFollow>().followDistance = friendList.Count * 0.5f;



        }
         */
    }

}
