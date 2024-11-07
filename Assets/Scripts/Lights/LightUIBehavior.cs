using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUIBehavior : MonoBehaviour
{
    public Player_Script player;
    [SerializeField] List<Sprite> lightSprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int lightnum;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.currentLightSource == "" && spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;
        }
        else if (player.currentLightSource != "" && spriteRenderer.enabled == false)
        {
            spriteRenderer.enabled = true;
            SwitchLights();

        }
    }

    void SwitchLights()
    {
        if(player.currentLightSource.Equals("Flashlight"))
        {
            spriteRenderer.sprite = lightSprites[0];
            lightnum = 1;
        }
        if (player.currentLightSource.Equals("Candle"))
        {
            spriteRenderer.sprite = lightSprites[1];
            lightnum = 2;
        }
        if (player.currentLightSource.Equals("Fireflies"))
        {
            spriteRenderer.sprite = lightSprites[2];
            lightnum = 3;
        }
    }
}
