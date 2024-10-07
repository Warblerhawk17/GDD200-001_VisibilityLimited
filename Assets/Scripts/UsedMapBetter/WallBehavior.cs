using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    // Store the original color of the wall's sprite
    private Color originalColor;
    private SpriteRenderer wallSprite;

    // Opacity value (set to 50%)
    public float transparentValue;
    private float originalAlpha = 1f;

    void Start()
    {
        // Get the SpriteRenderer component of the wall
        wallSprite = GetComponent<SpriteRenderer>();

        // Store the original color of the wall
        if (wallSprite != null)
        {
            originalColor = wallSprite.color;
        }
    }

    // Detect when the player enters the trigger area behind the wall
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is the object entering the trigger
        if (other.CompareTag("Player"))
        {
            SetWallTransparency(transparentValue);
        }
    }

    // Detect when the player exits the trigger area
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is the object exiting the trigger
        if (other.CompareTag("Player"))
        {
            SetWallTransparency(originalAlpha);
        }
    }

    // Set the transparency of the wall by adjusting its alpha value
    void SetWallTransparency(float alpha)
    {
        if (wallSprite != null)
        {
            Color newColor = originalColor;
            newColor.a = alpha; // Set the alpha value
            wallSprite.color = newColor; // Apply the new color with transparency
        }
    }
}