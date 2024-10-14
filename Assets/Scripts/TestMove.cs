using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AStarManager;

public class TestMove : MonoBehaviour
{
    public GameObject instance;
    public GameObject begining;
    public GameObject end;
    void start()
    {
        List<Node> list = instance.GetComponent<AStarManager>().GeneratePath(begining.GetComponent<Node>(), end.GetComponent<Node>());
        // List<Node> list = instance.GetComponent.GeneratePath(begining.GetComponent<Node>(), end.GetComponent<Node>());
         for (int i = 0; i < list.Count; i++)
        {
        transform.position = list[i].getPosition();
        }
    }
}
