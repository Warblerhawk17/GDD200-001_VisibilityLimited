using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    bool key_up = false,
        key_down = false,
        key_left = false,
        key_right = false;

    public float moveSpeed;

    BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        key_up = Input.GetKey(KeyCode.W);
        key_down = Input.GetKey(KeyCode.S);
        key_left = Input.GetKey(KeyCode.A);
        key_right = Input.GetKey(KeyCode.D);

        //calc movement
        float hSpeed = (key_right ? 1 : 0) - (key_left ? 1 : 0);
        float vSpeed = (key_up ? 1 : 0) - (key_down ? 1 : 0);

        hSpeed *= Time.deltaTime * moveSpeed;
        vSpeed *= Time.deltaTime * moveSpeed;

        Vector3 pos = transform.position;

        //horizonal collision
        if (Physics2D.OverlapBox(new Vector2(pos.x + hSpeed, pos.y), boxCollider.size, 0, LayerMask.GetMask("Wall"))){
            hSpeed = 0;
        }
        pos.x += hSpeed;

        //vertical collision
        if (Physics2D.OverlapBox(new Vector2(pos.x, pos.y + vSpeed), boxCollider.size, 0, LayerMask.GetMask("Wall"))){
            vSpeed = 0;
        }
        pos.y += vSpeed;

        transform.position = pos;
    }
}
