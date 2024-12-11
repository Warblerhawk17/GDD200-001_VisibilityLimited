using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControl : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject tutTrigger;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.transform.position.x > -30)
        {
            tutorial.SetActive(false);
            tutTrigger.SetActive(false);
        }
    }
}
