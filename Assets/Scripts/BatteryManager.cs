using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] flashlightBatterySprites;
    public Sprite[] candleBatterySprites;
    public Sprite[] firefliesBatterySprites;
    public List<Sprite> batterySprites;
    public float batteryCharge;
    int lightNum = 0;

    // Timer and interval to decrease charge every second
    public float interval = 1f;
    private float timer = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NaturalDepletion();
        ChangeBattery();
    }

    void ChangeBattery()
    {
        if (lightNum == 1)
        {
            switch (batteryCharge)
            {
                case >= 50f:
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = batterySprites[0];
                    break;
                case >= 40f:
                    spriteRenderer.sprite = batterySprites[1];
                    break;
                case >= 30f:
                    spriteRenderer.sprite = batterySprites[2];
                    break;
                case >= 20f:
                    spriteRenderer.sprite = batterySprites[3];
                    break;
                case >= 10f:
                    spriteRenderer.sprite = batterySprites[4];
                    break;
                case > 0f:
                    spriteRenderer.sprite = batterySprites[5];
                    break;
                case <= 0f:
                    spriteRenderer.enabled = false;
                    break;
                default:
                    break;
            }
        }
        else if (lightNum == 2)
        {
            switch (batteryCharge)
            {
                case >= 90f:
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = batterySprites[0];
                    break;

                case >= 80f:
                    spriteRenderer.sprite = batterySprites[1];
                    break;
                case >= 70f:
                    spriteRenderer.sprite = batterySprites[2];
                    break;
                case >= 60f:
                    spriteRenderer.sprite = batterySprites[3];
                    break;
                case >= 50f:
                    spriteRenderer.sprite = batterySprites[4];
                    break;
                case >= 40f:
                    spriteRenderer.sprite = batterySprites[5];
                    break;
                case >= 30f:
                    spriteRenderer.sprite = batterySprites[6];
                    break;
                case >= 20f:
                    spriteRenderer.sprite = batterySprites[7];
                    break;
                case >= 10f:
                    spriteRenderer.sprite = batterySprites[8];
                    break;
                case > 0f:
                    spriteRenderer.sprite = batterySprites[9];
                    break;
                case <= 0f:
                    spriteRenderer.enabled = false;
                    break;
                default:
                    break;
            }
        }
        else if (lightNum == 3)
        {
            switch (batteryCharge)
            {
                case >= 90f:
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = batterySprites[0];
                    break;

                case >= 80f:
                    spriteRenderer.sprite = batterySprites[1];
                    break;
                case >= 70f:
                    spriteRenderer.sprite = batterySprites[2];
                    break;
                case >= 60f:
                    spriteRenderer.sprite = batterySprites[3];
                    break;
                case >= 50f:
                    spriteRenderer.sprite = batterySprites[4];
                    break;
                case >= 40f:
                    spriteRenderer.sprite = batterySprites[5];
                    break;
                case >= 30f:
                    spriteRenderer.sprite = batterySprites[6];
                    break;
                case >= 20f:
                    spriteRenderer.sprite = batterySprites[7];
                    break;
                case >= 10f:
                    spriteRenderer.sprite = batterySprites[8];
                    break;
                case > 0f:
                    spriteRenderer.sprite = batterySprites[9];
                    break;
                case <= 0f:
                    spriteRenderer.enabled = false;
                    break;
                default:
                    break;
            }
        }

    }

    void NaturalDepletion()
    {
        timer += Time.deltaTime;

        // Check if the timer has reached or exceeded the interval
        if (timer >= interval)
        {
            timer = 0f;  
            if(batteryCharge > 0f)
            {
                batteryCharge--;
            }
            else
            {
                batteryCharge = 0f;
            }
        }
    }

    void SetBatteryType()
    {
        if (GameObject.FindWithTag("Flashlight"))
        {
            batterySprites = flashlightBatterySprites.OfType<Sprite>().ToList();
            lightNum = 1;
        }

        else if (GameObject.FindWithTag("Candle"))
        {
            batterySprites = candleBatterySprites.OfType<Sprite>().ToList();
            lightNum = 2;

        }
        else if (GameObject.FindWithTag("Fireflies"))
        {
            batterySprites = firefliesBatterySprites.OfType<Sprite>().ToList();
            lightNum = 3;

        }
    }


}
