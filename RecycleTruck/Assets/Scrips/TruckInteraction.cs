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

    private void Start()
    {
        // Find and store the GameManager script
        _gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        if (_gameManager.CurrentGameState == GameState.Idle)
        {
            _gameManager.StartGame();
            Debug.Log("Game state: Playing");
        }
    }
}


