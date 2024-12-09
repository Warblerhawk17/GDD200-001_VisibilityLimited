using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreamBehavior : MonoBehaviour
{
    public GameObject target;
    public LayerMask layerMask;
    public float explodeRadius;
    public float timeToTeleport;

    private Animator anim;
    private AudioSource bomb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is close enough and in line of sight
        //if so see if they have the candle, if they do do nothing
        //if they don't scream and deal lots of damage then teleport
        //teleport after a bit of time
        anim.SetBool("isExploding", false);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position - transform.position, explodeRadius, layerMask);
        if (hit && hit.collider.gameObject.layer == target.gameObject.layer && target.GetComponent<player_script>().currentLightSource != "Candle")
        {
            anim.SetBool("isExploding", true);
            bomb.Play();
            target.GetComponent<player_script>().batteryManager.batteryCharge -= 30;
            if (target.GetComponent<player_script>().currentLightSource == "")
            {
                
                target.GetComponent<player_script>().LoseLife();
            }
            teleport();
        }
        timeToTeleport -= Time.deltaTime;
        if (timeToTeleport < 0) 
        {
            teleport();
            timeToTeleport = 30;
        }
    }

    void teleport()
    {
        List<Node> nodes = AStarManager.instance.NodesInScene().ToList();
        Node node = nodes[Random.Range(0,nodes.Count())];
        while (Vector2.Distance(node.transform.position, target.transform.position) < 15)
        {
            nodes.Remove(node);
            node = nodes[Random.Range(0, nodes.Count())];
        }
        transform.position = node.transform.position;
    }
}
