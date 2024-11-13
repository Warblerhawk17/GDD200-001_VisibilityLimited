using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSubBehavior : MonoBehaviour
{

    ShadowBehavior shadow;
    // Start is called before the first frame update
    void Start()
    {
            shadow = GameObject.Find("Shadow").GetComponent<ShadowBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shadow"))
        {

                shadow.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Shadow") && shadow.GetComponent<ShadowBehavior>().speed == 0)
        {
            shadow.speed = 3;
        }
    }
}
