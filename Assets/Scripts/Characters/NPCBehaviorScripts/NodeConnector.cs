using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NodeConnector : MonoBehaviour
{
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        ConnectNodes();
    }

    // Update is called once per frame
    void ConnectNodes()
    {
        foreach (Node n in AStarManager.instance.NodesInScene()) 
        {
            foreach (Node m in AStarManager.instance.NodesInScene())
            {
                if (n != m)
                {
                    RaycastHit2D hit = Physics2D.CircleCast(n.transform.position, 0.1f, m.transform.position - n.transform.position, 
                        Vector2.Distance(n.transform.position, m.transform.position), layerMask);
                    if (!hit) 
                    {
                        n.connections.Add(m);
                    }
                }
            }
        }
    }
}
