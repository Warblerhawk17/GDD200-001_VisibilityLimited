//Made by Onyx

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public static AStarManager instance; //A instance of AStarManager to be used by other scripts

    private void Awake() //On Awake sets instance to this
    {
        instance = this;
    }

    public List<Node> GeneratePath(Node start, Node end) //Generates a list to be used as a path from a start node to an end node
    {
        List<Node> openSet = new List<Node>(); //the list of nodes that can be checked

        foreach(Node n in FindObjectsOfType<Node>()) //sets the gScore of all nodes to maxValue
        {
            n.gScore = float.MaxValue;
        }

        //set the g and h scores for start
        start.gScore = 0; //G is the distance to start, this is start so it is 0
        start.hScore = Vector2.Distance(start.transform.position, end.transform.position); //H is distance to the end

        openSet.Add(start); //adds start to the open list

        while (openSet.Count > 0) //loops while openSet has things in it
        {
            int lowestF = 0; //index of the lowest F score
            for (int i = 0; i < openSet.Count; i++) //goes through openSet
            {
                if (openSet[i].FScore() < openSet[lowestF].FScore()) //if the F of i is less than the current F you swap to the new lowest F
                {
                    lowestF = i;
                }
            }

            Node currentNode = openSet[lowestF]; //uses the lowest F as the curent node
            openSet.Remove(currentNode); //removes currentNode from openSet

            if (currentNode == end) //if our currentNode is the same as the end we return the path
            {
                List<Node> path= new List<Node>();
                path.Insert(0, end); //builds list from the end to the start
                while (currentNode != start) 
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }
                path.Reverse(); //reverses the path so that its in the right order
                return path;
            }
            foreach (Node connectedNode in currentNode.connections) //searches the connections of the current node to add to the openSet if they meet the right conditions
            {
                float heldGScore = currentNode.gScore + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);
                
                if (heldGScore < connectedNode.gScore)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gScore = heldGScore;
                    connectedNode.hScore = Vector2.Distance(connectedNode.transform.position, end.transform.position);

                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                }
            }

        }

        return null; //returns null if openset is empty, aka something went wrong
    }

    public Node FindNearestNode(Vector2 position) //finds the nearest node from a given position
    {
        Node foundNode = null;
        float minDistance = float.MaxValue;
        foreach (Node node in NodesInScene())
        {
            float currentDistance = Vector2.Distance(position, node.transform.position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                foundNode = node;
            }

        }
        return foundNode;
    }

    public Node FindFurthestNode(Vector2 position) //finds the furthest node from a given position
    {
        Node foundNode = null;
        float maxDistance = 0;

        foreach (Node node in NodesInScene())
        {
            float currentDistance = Vector2.Distance(position, node.transform.position);
            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                foundNode = node;
            }
        }
        return foundNode;
    }

    public Node[] NodesInScene() //helper function to get the nodes in a scene
    {
        return FindObjectsOfType<Node>();
    }


}
