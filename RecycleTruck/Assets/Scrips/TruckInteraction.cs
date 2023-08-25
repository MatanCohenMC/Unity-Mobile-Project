using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine;

public class TruckInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private SpawnManager _spawnManager;
    //[SerializeField] private Vector3 _currentPosition;

    private void Start()
    {
        // Find and store the GameManager script
        _gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        /*if(_gameManager.CurrentGameState == GameState.Playing)
        {
            Vector3 newPosition = _currentPosition;
            newPosition.y = _currentPosition.y+2;
            transform.DOMove(newPosition, 1f).SetEase(Ease.OutBack);
        }*/
    }

    void OnMouseDown()
    {
        if (_gameManager.CurrentGameState == GameState.Idle)
        {
            _gameManager.StartGame();
            Debug.Log("Game state: Playing");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _spawnManager.SpawnTriggerEntered();
    }
}


