using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> monsterList = new List<GameObject>();
    public static MonsterSpawner instance;
    public int incrementAmmount = 1;
    public int currentIndex = 0;

    private void Awake() //On Awake sets instance to this
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMonsters(int i)
    {
        
        if (i < monsterList.Count && monsterList[i] != null)
        {
            monsterList[i].gameObject.SetActive(true);
            monsterList[i].gameObject.transform.position = getSpawnNode();
            Debug.Log("Spawn monster #" + i);
        }
    }
    public void SpawnMonsters()
    {
        for (int i = currentIndex; i < currentIndex + incrementAmmount; i++) 
        { 
            SpawnMonsters(i);
        }
        currentIndex = currentIndex + incrementAmmount;
    }
    private Vector2 getSpawnNode()
    {
        List<Node> nodes = AStarManager.instance.NodesInScene().ToList();
        Node node = nodes[Random.Range(0, nodes.Count())];
        while (Vector2.Distance(node.transform.position, GameObject.Find("Player").transform.position) < 15)
        {
            nodes.Remove(node);
            node = nodes[Random.Range(0, nodes.Count())];
        }
        return node.transform.position;
    }
}
