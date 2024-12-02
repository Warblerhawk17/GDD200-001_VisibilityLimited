using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSpawner : MonoBehaviour
{
    public List<Vector2> spawnLocations = new List<Vector2>();
    public List<GameObject> friends = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < friends.Count; i++)
        {
            int index = Random.Range(0, spawnLocations.Count);
            Vector2 location = spawnLocations[index];
            spawnLocations.RemoveAt(index);
            friends[i].transform.position = location;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
