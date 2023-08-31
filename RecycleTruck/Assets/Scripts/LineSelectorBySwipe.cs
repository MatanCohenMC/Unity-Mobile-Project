using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

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
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;
    private GameManager _gameManager;
    private Tween _currentTween;
    private LanePosition _currentLane;
    private float _offset = 50f;
    private float _animationDuration = 1.7f;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _currentLane = LanePosition.Middle;
    }

    private void Update()
    {
        if (_gameManager.CurrentGameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _endTouchPosition = Input.mousePosition;
                ProcessSwipe();
            }
        }
    }

    private void ProcessSwipe()
    {
        Vector2 swipeDirection = GetSwipeDirection();
        float swipeDistance = swipeDirection.magnitude;

        if (swipeDistance > _offset)
        {
            Vector3 newPosition = _truck.position;
            float middleLaneX = _lanes[(int)LanePosition.Middle].position.x;

            if (swipeDirection.x < 0)
            {
                // Swipe to the left
                newPosition.x = _currentLane == LanePosition.Right ? middleLaneX : _lanes[(int)LanePosition.Left].position.x;
                _currentLane = _currentLane == LanePosition.Right ? LanePosition.Middle : LanePosition.Left;
            }
            else if (swipeDirection.x > 0)
            {
                // Swipe to the right
                newPosition.x = _currentLane == LanePosition.Left ? middleLaneX : _lanes[(int)LanePosition.Right].position.x;
                _currentLane = _currentLane == LanePosition.Left ? LanePosition.Middle : LanePosition.Right;
            }

            StartCoroutine(PlayMoveAnimation(newPosition));
        }
    }

    private Vector2 GetSwipeDirection()
    {
        return _endTouchPosition - _startTouchPosition;
    }

    private IEnumerator PlayMoveAnimation(Vector3 targetPosition)
    {
        yield return new WaitForSeconds(0.12f); // Delay before animation starts
        _currentTween = _truck.DOMove(targetPosition, _animationDuration).SetEase(Ease.OutBack).OnComplete(() => _currentTween = null);
        _currentTween.Play();

        yield return new WaitForSeconds(2.5f); // Wait for animation duration
    }
}