//Made by Onyx

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom; //The node this node came from
    public List<Node> connections; //The nodes connected to this one

    public float gScore; //Distance from the node to the start
    public float hScore; //Distance from the node to the end

    public float FScore() //Combination of gScore and hScore
    {
        return gScore+hScore;
    }

    public Vector3 getPosition() //Helper funcion to get the position of the node
    {
        return transform.position;
    }

    private void OnDrawGizmos() //Helper function which draws lines between the node and its connected nodes 
    {
        Gizmos.color = Color.green;
        if (connections.Count > 0) 
        {
            for (int i = 0; i < connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }

}
