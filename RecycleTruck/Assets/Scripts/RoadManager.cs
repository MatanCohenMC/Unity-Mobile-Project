using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private float m_InitialScrollSpeed = 5f;
    private Vector3 _initialPosition;
    private float _currentScrollSpeed;
    private float _timer;


    private void Start()
    {
        _initialPosition = transform.position;
        _currentScrollSpeed = m_InitialScrollSpeed;
        _timer = 0f;
    }

    private void Update()
    {
        GameState currentGameState = GameManager.Instance.CurrentGameState;

        if (currentGameState == GameState.Playing)
        {
            _timer += Time.deltaTime; // Update the timer when in Playing state
            parallaxScrolling();
        }
    }

    // This method implements parallax scrolling.
    private void parallaxScrolling()
    {
        float offset = _timer * _currentScrollSpeed; // Use the cached timer value

        // Apply the offset to the background's position with a decreasing z value
        Vector3 newPosition = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z - offset);
        transform.position = newPosition;
    }
}