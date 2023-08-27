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
    [SerializeField] private Vector3 _initPosition;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

   

    void OnMouseDown()
    {
        if (_gameManager.CurrentGameState == GameState.Idle)
        {
            _gameManager.StartGame();
            transform.position = _initPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _spawnManager.SpawnTriggerEntered();
    }
}


