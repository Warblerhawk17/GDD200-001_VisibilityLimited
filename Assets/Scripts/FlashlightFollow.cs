// Amelia Nehring
// GDD 200-001
// Fall 2024

using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Transform player;  // Variable to assign to the player object for it to "orbit" the player properly
    public float orbitDistance = 1.5f; // Distance the flashlight should be from the player
    public float rotationSpeed = 5f;   // Speed that the flashlight will rotate
    public float flashlightZPos = -0.1f; // Keep flashlight Z-axis position at -.1 for light to be above the floor
                                         // (VERY IMPORTANT!! Z AXIS POS MUST BE <= -0.1 FOR IT TO WORK PROPERLY)

    void Start()
    {

    }

    void Update()
    {
        // Get the mouse coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Z-axis positon

        // Calculate where the flashlight should move based on the player position
        Vector3 directionToMouse = (mousePosition - player.position).normalized;

        // Calculate the new position for the flashlight
        Vector3 newPosition = player.position + directionToMouse * orbitDistance;

        // Update the flashlight position
        transform.position = newPosition;

        // Calculate the angle to the mouse cursor
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Rotate the flashlight to point towards the mouse cursor
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Apply the rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, flashlightZPos); 
    }
}
