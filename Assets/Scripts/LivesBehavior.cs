using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBehavior : MonoBehaviour
{
    BatteryManager batteryManager;
    public List<Sprite> livesSprites;
    public SpriteRenderer livesRenderer;
    public Player_Script player;
   [SerializeField] int spriteNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        livesRenderer.sprite = livesSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoseLife()
    {


        if (spriteNum != 3)
        { 
           spriteNum++;

        }
        else
        {
            livesRenderer.sprite = livesSprites[3];
            Debug.Log("GAME OVER!");
        }
        livesRenderer.sprite = livesSprites[spriteNum];

    }
}
