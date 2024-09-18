using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    private float Sprintspeed = 8.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 vector3 = new(horizontalInput, verticalInput, 0);
        Vector3 direction = vector3;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.Translate(Sprintspeed * Time.deltaTime * direction);

        }
        else
        {
            transform.Translate(speed * Time.deltaTime * direction);

        }

    }
}
