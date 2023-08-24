using UnityEngine;
using DG.Tweening;

public class LineSelector : MonoBehaviour
{
    [SerializeField] private Transform _truck;

    private GameManager _gameManager;

    private void Start()
    {
        // Find and store the GameManager script
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("CurrentGameState:" + _gameManager.CurrentGameState);

        if (_gameManager.CurrentGameState == GameState.Playing)
        {
            Vector3 newPosition = _truck.position;
            newPosition.z = transform.position.z;
            _truck.DOMove(newPosition, 2f).SetEase(Ease.OutBack);
        }
    }
}