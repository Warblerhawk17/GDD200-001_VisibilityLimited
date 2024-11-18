using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBehavior : MonoBehaviour
{
    BatteryManager batteryManager;
    public List<Sprite> livesSprites;
    public player_script player;
   [SerializeField] int spriteNum = 0;
   public UnityEngine.UI.Image livesImage;


    // Start is called before the first frame update
    void Start()
    {
        livesImage.sprite = livesSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoseLife()
    {

        Debug.Log("LoseLives behavior called");
        if (spriteNum != 3)
        { 
           spriteNum++;

        }
        else
        {
            livesImage.sprite = livesSprites[3];
            Debug.Log("GAME OVER!");
        }
        livesImage.sprite = livesSprites[spriteNum];

    }
}
