using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FriendFollow : MonoBehaviour
{
    public GameObject followTarget;
    public float followDistance;
    public LayerMask layerMask;
    public Animator anim;
    public bool pickedUp = false; //bool for if they have been picked up before 
    public List<GameObject> dialog;
    public List<Sprite> characterHead;
    private player_script player_Script;
    private bool wasCalled = false;
    private Node currentNode; //the current node it is at
    private List<Node> path = new List<Node>(); //the path of nodes it will travel
    private float speed = 4;
    private player_script player_script;
    private GameObject curFriend;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); //Animator object
        player_Script = GameObject.Find("Player").GetComponent<player_script>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTarget != null)
        {
            anim.SetBool("isWalking", true);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.Normalize(followTarget.transform.position - transform.position), float.MaxValue, layerMask);
            if (hit.collider.gameObject != followTarget.gameObject)
            {
                //Debug.Log("Non Direct Move");
                if (path.Count == 0)
                { //make new path
                    Node nearestNode = AStarManager.instance.FindNearestNode(transform.position); //the node nearest to the friend
                    Node targetNode = AStarManager.instance.FindNearestNode(followTarget.transform.position);
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
            else if (Vector2.Distance(transform.position, followTarget.transform.position) > followDistance)
            {
                //Debug.Log("Direct Move");
                GoTowards(followTarget.transform.position);
                path.Clear();
            }
            else
            {
                anim.SetBool("isWalking", false);
                //Debug.Log("is not walking");
            }

        }
        //else anim.SetBool("isWalking", false);
    }

    private void GoTowards(Vector2 goTo) //helper method which both moves and rotates the thing moving
    {
        transform.position = Vector2.MoveTowards(this.transform.position, goTo, speed * Time.deltaTime); //moves the npc
        Vector2 direction = goTo - (Vector2)transform.position; // finds direction between npc and target
        direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
        //Debug.Log(angle);
        anim.SetBool("isWalking", true);
        if (45 < angle && angle <= 135) //facing up (W)
        {
            //Debug.Log("facing Up");
            anim.SetBool("facingHoriz", false);
            anim.SetBool("facingUp", true);
            anim.SetBool("facingLeft", false);
        }
        else if (-135 > angle || angle > 135) //facing left (A)
        {
            //Debug.Log("facing Left");
            anim.SetBool("facingHoriz", true);
            anim.SetBool("facingUp", false);
            anim.SetBool("facingLeft", true);
        }
        else if (-45 > angle && angle >= -135) //facing down (S)
        {
            //Debug.Log("facing Down");
            anim.SetBool("facingHoriz", false);
            anim.SetBool("facingUp", false);
            anim.SetBool("facingLeft", false);
        }
        else //facing right (D)
        {
            //Debug.Log("facing Right");
            anim.SetBool("facingHoriz", true);
            anim.SetBool("facingUp", false);
            anim.SetBool("facingLeft", false);
        }
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes npc rotation
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.GetComponent<player_script>().friendList.Contains(this.gameObject))
        {
            player_script player = collision.GetComponent<player_script>();
            if (player.friendList.Count < 2)
            {
                player.friendList.Add(this.gameObject);
                StartCoroutine(PlayDialog());
                followTarget = player.gameObject;
                followDistance = player.friendList.Count * 0.5f;
                curFriend = this.gameObject;
                if (!pickedUp)
                {
                    pickedUp = true;
                    player.friendsPickedUp++;
                    MonsterSpawner.instance.SpawnMonsters(player.friendsPickedUp - 1);
                }
            }
        }
    }

    private IEnumerator PlayDialog()
    {
        wasCalled = true;
        Debug.Log("Dialog was called");
        if (player_Script.friendsSaved == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                dialog[i].SetActive(true);
                yield return new WaitForSeconds(3);
                dialog[i].SetActive(false);
            }
        }
        else if (player_Script.friendsSaved == 1)
        {
            for (int i = 3; i < 5; i++)
            {
                dialog[i].SetActive(true);
                yield return new WaitForSeconds(3);
                dialog[i].SetActive(false);
            }
        }
        else if (player_Script.friendsSaved == 2)
        {
            dialog[5].SetActive(true);
            yield return new WaitForSeconds(3);
            dialog[5].SetActive(false);
        }

        yield return new WaitForSeconds(10);
    }
}
