using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.0f; // Adjust this to control the scrolling speed
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the amount to move the background based on time and scrollSpeed
        float offset = Time.time * scrollSpeed;

        // Apply the offset to the background's position with a decreasing z value
        Vector3 newPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - offset);
        transform.position = newPosition;
    }
}