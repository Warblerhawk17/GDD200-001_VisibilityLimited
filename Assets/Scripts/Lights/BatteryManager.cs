using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BatteryManager : MonoBehaviour
{
   // public SpriteRenderer spriteRenderer;
    public Sprite[] flashlightBatterySprites;
    public Sprite[] candleBatterySprites;
    public Sprite[] firefliesBatterySprites;
    public List<Sprite> batterySprites;
    public float batteryCharge;
    [SerializeField] int lightNum = 0;
    [SerializeField] UnityEngine.UI.Image batteryImage;

    // Timer and interval to decrease charge every second
    public float interval = 1f;
    private float timer = 0f;

    void Start()
    {
        batteryImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (batteryCharge <= 0)
        {
            batteryImage.enabled = false;

        }
        SetBatteryType();
        NaturalDepletion();
        ChangeBattery();
    }

    void ChangeBattery()
    {
        if (lightNum == 1)
        {
            switch (batteryCharge)
            {
                case >= 40f:
                    batteryImage.enabled = true;
                    batteryImage.sprite = batterySprites[0];
                    break;
                case >= 30f:
                    batteryImage.sprite = batterySprites[1];
                    break;
                case >= 20f:
                    batteryImage.sprite = batterySprites[2];
                    break;
                case >= 10f:
                    batteryImage.sprite = batterySprites[3];
                    break;
                case >= 0f:
                    batteryImage.sprite = batterySprites[4];
                    break;
                case <= 0f:
                    batteryImage.enabled = false;
                    break;
                default:
                    break;
            }
        }
        else if (lightNum == 2)
        {
            switch (batteryCharge)
            {
                case >= 140f:
                    batteryImage.enabled = true;
                    batteryImage.sprite = batterySprites[0];
                    break;
                case >= 130f:
                    batteryImage.sprite = batterySprites[1];
                    break;
                case >= 120f:
                    batteryImage.sprite = batterySprites[2];
                    break;
                case >= 110f:
                    batteryImage.sprite = batterySprites[3];
                    break;
                case >= 100f:
                    batteryImage.sprite = batterySprites[4];
                    break;
                case >= 90f:
                    batteryImage.sprite = batterySprites[5];
                    break;

                case >= 80f:
                    batteryImage.sprite = batterySprites[6];
                    break;
                case >= 70f:
                    batteryImage.sprite = batterySprites[7];
                    break;
                case >= 60f:
                    batteryImage.sprite = batterySprites[8];
                    break;
                case >= 50f:
                    batteryImage.sprite = batterySprites[9];
                    break;
                case >= 40f:
                    batteryImage.sprite = batterySprites[10];
                    break;
                case >= 30f:
                    batteryImage.sprite = batterySprites[11];
                    break;
                case >= 20f:
                    batteryImage.sprite = batterySprites[12];
                    break;
                case >= 10f:
                    batteryImage.sprite = batterySprites[13];
                    break;
                case > 0f:
                    batteryImage.sprite = batterySprites[14];
                    break;
                case <= 0f:
                    batteryImage.enabled = false;
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
                    batteryImage.enabled = true;
                    batteryImage.sprite = batterySprites[0];
                    break;

                case >= 80f:
                    batteryImage.sprite = batterySprites[1];
                    break;
                case >= 70f:
                    batteryImage.sprite = batterySprites[2];
                    break;
                case >= 60f:
                    batteryImage.sprite = batterySprites[3];
                    break;
                case >= 50f:
                    batteryImage.sprite = batterySprites[4];
                    break;
                case >= 40f:
                    batteryImage.sprite = batterySprites[5];
                    break;
                case >= 30f:
                    batteryImage.sprite = batterySprites[6];
                    break;
                case >= 20f:
                    batteryImage.sprite = batterySprites[7];
                    break;
                case >= 10f:
                    batteryImage.sprite = batterySprites[8];
                    break;
                case > 0f:
                    batteryImage.sprite = batterySprites[9];
                    break;
                case <= 0f:
                    batteryImage.enabled = false;
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
