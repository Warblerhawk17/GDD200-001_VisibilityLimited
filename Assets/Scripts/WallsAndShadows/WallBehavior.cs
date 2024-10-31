using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBehavior : MonoBehaviour
{
    // Store the original color of the wall's sprite
    private Color originalColor;
    private Tilemap wallSprite;

    // Opacity value (set to 50%)
    public float transparentValue = 0.5f;
    public Transform player;

    void Start()
    {

        wallSprite = GetComponent<Tilemap >();

        // Store the original color of the wall
        if (TryGetComponent<Tilemap>(out wallSprite))
        {
            wallSprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

   private void Update()
    {
        if (player != null && wallSprite != null)
        {
            if (IsPlayerAboveWall())
            {
                SetWallTransparency(transparentValue);  // Make wall transparent
            }
            else
            {
                SetWallTransparency(1f);  // Reset to original transparency
            }
        }
    }

    // Detect when the player enters the trigger area behind the wall
   void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player is the object entering the trigger
        if (collision.CompareTag("Player"))
        {
            if (player != null && wallSprite != null)
            {
                if (IsPlayerAboveWall())
                {
                    SetWallTransparency(transparentValue);  // Make wall transparent
                }
                else
                {
                    SetWallTransparency(1f);  // Reset to original transparency
                }
            }
        }
    }

   /* void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player is the object exiting the trigger
        if (collision.CompareTag("Player"))
        {
            SetWallTransparency(1f);
        }
    }*/

    bool IsPlayerAboveWall()
    {
        float wallYPosition = transform.position.y;
        
        return player.position.y > wallYPosition;
    }

    // Set the transparency of the wall by adjusting its alpha value
    void SetWallTransparency(float alpha)
    {
        if (wallSprite != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha; // Set the alpha value
            wallSprite.color = new Color(1f, 1f, 1f, transparentValue); // Apply the new color with transparency
        }
    }
}