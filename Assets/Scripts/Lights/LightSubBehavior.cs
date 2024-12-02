using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSubBehavior : MonoBehaviour
{
    ShadowBehavior shadow;

    void Update()
    {
        if (shadow == null)
        {
            shadow = GameObject.Find("Shadow").GetComponent<ShadowBehavior>();
            Debug.Log("Trying for Shadow");
        }
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
