using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum LanePosition
{
    Left,
    Middle,
    Right
}

public class LineSelectorBySwipe : MonoBehaviour
{
    [SerializeField] private Transform _truck;
    [SerializeField] private Transform[] _lanes;
    private Vector2 m_StartTouchPosition;
    private Vector2 m_EndTouchPosition;
    private GameManager m_GameManager;
    private Tween m_CurrentTween;
    private LanePosition m_CurrentLane;
    private float m_Offset = 50f;
    private float m_AnimationDuration = 1.7f;

    private void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        m_CurrentLane = LanePosition.Middle;
    }

    private void Update()
    {
        // handles swipe input when the game is in the 'Playing' state.
        if (m_GameManager.CurrentGameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_StartTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                m_EndTouchPosition = Input.mousePosition;
                processSwipe();
            }
        }
    }

    // this method processes a swipe gesture, determining its direction and moving the truck accordingly.
    private void processSwipe()
    {
        Vector2 swipeDirection = getSwipeDirection();
        float swipeDistance = swipeDirection.magnitude;

        if (swipeDistance > m_Offset)
        {
            Vector3 newPosition = _truck.position;
            float middleLaneX = _lanes[(int)LanePosition.Middle].position.x;

            if (swipeDirection.x < 0)
            {
                // Swipe to the left
                newPosition.x = m_CurrentLane == LanePosition.Right ? middleLaneX : _lanes[(int)LanePosition.Left].position.x;
                m_CurrentLane = m_CurrentLane == LanePosition.Right ? LanePosition.Middle : LanePosition.Left;
            }
            else if (swipeDirection.x > 0)
            {
                // Swipe to the right
                newPosition.x = m_CurrentLane == LanePosition.Left ? middleLaneX : _lanes[(int)LanePosition.Right].position.x;
                m_CurrentLane = m_CurrentLane == LanePosition.Left ? LanePosition.Middle : LanePosition.Right;
            }

            StartCoroutine(playMoveAnimation(newPosition));
        }
    }

    // this method calculates and returns the direction of a swipe gesture as a vector.
    private Vector2 getSwipeDirection()
    {
        return m_EndTouchPosition - m_StartTouchPosition;
    }

    // this method initiates a move animation of the truck to the specified target position.
    private IEnumerator playMoveAnimation(Vector3 targetPosition)
    {
        m_CurrentTween = _truck.DOMove(_truck.position, 0.2f); // Kind of Delay before animation starts
        m_CurrentTween = _truck.DOMove(targetPosition, m_AnimationDuration).SetEase(Ease.OutBack).OnComplete(() => m_CurrentTween = null);
        m_CurrentTween.Play();

        yield return new WaitForSeconds(2.5f); // Wait for animation duration
        //yield return new WaitForSeconds(m_AnimationDuration); // Wait for animation duration
    }
}