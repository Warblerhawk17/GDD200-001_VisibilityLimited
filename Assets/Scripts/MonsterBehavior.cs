//Made by Onyx

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public GameObject target; //what the monster will follow
    public float speed; //how fast it will move
    public float chaseLimit; // how close the monster can chase the player

    private float distance; // distance between monster and target
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.transform.position); // gets distance between monster and target
        if (distance < chaseLimit)
        {
            Vector2 direction = target.transform.position - transform.position; // finds direction between monster and target
            direction.Normalize(); // normalizes direction (keeps direction, sets length to 1, makes the math work)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // weird math to find the angle, yes the Atan2 goes y first then x
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime); // changes monster position
            transform.rotation = Quaternion.Euler(Vector3.forward * angle); // changes monster rotation
        }
    }
}
