using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1.0f;
    private GameManager _gameManager;
    private Vector3 _initialPosition;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if(_gameManager.CurrentGameState == GameState.Playing)
        {
            // Calculate the amount to move the background based on time and scrollSpeed
            float offset = Time.time * scrollSpeed;

            // Apply the offset to the background's position with a decreasing z value
            Vector3 newPosition = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z - offset);
            transform.position = newPosition;
        }
    }
}