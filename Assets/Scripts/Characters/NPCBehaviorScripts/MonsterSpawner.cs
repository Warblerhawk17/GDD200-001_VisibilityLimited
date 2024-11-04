using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> monsterList = new List<GameObject>();
    public static MonsterSpawner instance;

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
        //Instantiate(monsterList[i], AStarManager.instance.FindFurthestNode(GameObject.Find("Player").transform.position).transform);
        if (i < monsterList.Count)
        {
            monsterList[i].gameObject.SetActive(true);
            Debug.Log("Spawn monster");
        }
    }
}
