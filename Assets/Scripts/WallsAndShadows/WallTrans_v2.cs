using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTrans_v2 : MonoBehaviour
{

    private Color originalColor;  // Fixed the typo
    private Tilemap wallTilemap;

    public Transform player;

    public float wallYMod;
    public float transVal = 0.5f;
    private float origAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        wallTilemap = GetComponent<Tilemap>();

        if (wallTilemap != null)
        {
            originalColor = wallTilemap.color;  // Fixed typo here too
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // Check if the player is the object entering the trigger
        if (collision.CompareTag("Player"))
        {
            if (player != null && wallTilemap != null)
            {
                if (IsPlayerAboveWall())
                {
                    SetWallTransparency(transVal);  // Use transVal, not transparentValue
                }
                else
                {
                    SetWallTransparency(origAlpha);  // Reset to original transparency
                }
            }
        }
    }

    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        // Check if the player is the object entering the trigger
        if (collision.CompareTag("Player"))
        {
            SetWallTransparency(1f);
        }
    }

    bool IsPlayerAboveWall()
    {
        float wallYPosition = transform.position.y + 0.5f;

        return player.position.y > wallYPosition;
    }

    // Set the transparency of the wall by adjusting its alpha value
    void SetWallTransparency(float alpha)
    {
        if (wallTilemap != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha; // Set the alpha value
            wallTilemap.color = newColor;  // Apply the new color with transparency
        }
    }
}
