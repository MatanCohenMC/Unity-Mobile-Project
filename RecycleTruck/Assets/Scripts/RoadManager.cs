using System.Collections;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private float initialScrollSpeed = 5f;
    [SerializeField] private float speedIncreaseInterval = 10f; // Time interval to increase speed
    [SerializeField] private float speedIncreaseAmount = 1f; // Amount to increase speed
    private GameManager _gameManager;
    private Vector3 _initialPosition;
    private float _currentScrollSpeed;
    private float _timer;


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _initialPosition = transform.position;
        _currentScrollSpeed = initialScrollSpeed;
        _timer = 0f;
    }

    private void Update()
    {
        GameState currentGameState = _gameManager.CurrentGameState; // Cache the value

        if (currentGameState == GameState.Playing)
        {
            parallaxScrolling();
        }

        /*if (_gameManager.CurrentGameState == GameState.Playing)
        {
            Debug.Log("Road start moving");
            parallaxScrolling();
            
           

            // Update the timer
            *//*_timer += Time.deltaTime;

            // Check if it's time to increase the speed
            if (_timer >= speedIncreaseInterval)
            {
                _currentScrollSpeed += speedIncreaseAmount;
                StartChangingScrollSpeed(_currentScrollSpeed, 10f);
                _timer = 0f; // Reset the timer
            }*//*
        }*/
    }

    private void parallaxScrolling()
    {
        // Calculate the amount to move the background based on time and scrollSpeed
        float offset = Time.time * _currentScrollSpeed;

        // Apply the offset to the background's position with a decreasing z value
        Vector3 newPosition = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z - offset);
        transform.position = newPosition;
        Debug.Log($"transform.position: {transform.position}");
    }

    // Coroutine to gradually change the scroll speed over time
    private IEnumerator ChangeScrollSpeed(float newSpeed, float duration)
    {
        float elapsedTime = 0f;
        float startSpeed = _currentScrollSpeed;

        while (elapsedTime < duration)
        {
            _currentScrollSpeed = Mathf.Lerp(startSpeed, newSpeed, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentScrollSpeed = newSpeed; // Ensure final speed is accurate
    }

    // Example method to start changing the scroll speed
    private void StartChangingScrollSpeed(float newSpeed, float duration)
    {
        StartCoroutine(ChangeScrollSpeed(newSpeed, duration));
    }
}