using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUIBehavior : MonoBehaviour
{
    public player_script player;
    [SerializeField] List<Sprite> lightSprites;
    [SerializeField] public UnityEngine.UI.Image uiImage;
    string currentLight;

    private bool hasLight = false;

    // Start is called before the first frame update
    void Start()
    {
        uiImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLight)
        {
            if (player.currentLightSource == "" && uiImage.enabled)
            {
                uiImage.enabled = false;
            }
            else if (player.currentLightSource != "" && !uiImage.enabled)
            {
                uiImage.enabled = true;
                SwitchLights();
            }
        }
    }

    void SwitchLights()
    {
        if(player.currentLightSource.Equals("Flashlight"))
        {
            uiImage.sprite = lightSprites[0];
        }
        if (player.currentLightSource.Equals("Candle"))
        {
            uiImage.sprite = lightSprites[1];
        }
        if (player.currentLightSource.Equals("Fireflies"))
        {
            uiImage.sprite = lightSprites[2];
        }
    }
}
