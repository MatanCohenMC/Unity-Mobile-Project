using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeRightLeft : MonoBehaviour
{
    private GameManager _gameManager;
    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void Update()
    {
        if(_gameManager.CurrentGameState == GameState.Playing)
        {
            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _startTouchPosition = Input.GetTouch(0).position;
            }

            if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _endTouchPosition = Input.GetTouch(0).position;

                if(_endTouchPosition.x < _startTouchPosition.x)
                {
                    turnLeft();
                }
                
                if(_endTouchPosition.x > _startTouchPosition.x)
                {
                    turnRight();
                }
            }
        }
    }

    private void turnLeft()
    {

    }
    
    private void turnRight()
    {

    }
}
