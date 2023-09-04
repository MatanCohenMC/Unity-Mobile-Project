using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private float m_InitialScrollSpeed = 5f;
    private Vector3 m_InitialPosition;
    private float m_CurrentScrollSpeed;
    private float m_Timer;

    private void Start()
    {
        setInitValues();
    }

    // this method set init values
    private void setInitValues()
    {
        m_InitialPosition = transform.position;
        m_CurrentScrollSpeed = m_InitialScrollSpeed;
        m_Timer = 0f;
    }

    private void Update()
    {
        GameState currentGameState = GameManager.Instance.CurrentGameState;

        if (currentGameState == GameState.Playing)
        {
            m_Timer += Time.deltaTime; // Update the timer when in Playing state
            parallaxScrolling();
        }
    }

    // This method implements parallax scrolling.
    private void parallaxScrolling()
    {
        float offset = m_Timer * m_CurrentScrollSpeed; // Use the cached timer value

        // Apply the offset to the background's position with a decreasing z value
        Vector3 newPosition = new Vector3(m_InitialPosition.x, m_InitialPosition.y, m_InitialPosition.z - offset);
        transform.position = newPosition;
    }
}