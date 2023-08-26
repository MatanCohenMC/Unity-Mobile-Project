using UnityEngine;
using DG.Tweening;

public class LineSelector : MonoBehaviour
{
    [SerializeField] private Transform _truck;

    private GameManager _gameManager;
    private Tween _currentTween;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (_gameManager.CurrentGameState == GameState.Playing)
        {
            stopAndStartNewMovingLineAnimation();
        }
    }

    private void stopAndStartNewMovingLineAnimation()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
        }

        startMovingLineAnimation();
    }

    private void startMovingLineAnimation()
    {
        Vector3 newPosition = _truck.position;
        newPosition.x = transform.position.x;
        _currentTween = _truck.DOMove(newPosition, 2f).SetEase(Ease.OutBack);
    }
}