using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSelector : MonoBehaviour
{
    [SerializeField] private float _spaceBetweenLines = 5f;
    [SerializeField] private Transform _truck; // Reference to the Truck GameObject

    void OnMouseDown()
    {
        Vector3 newPosition = _truck.position;
        newPosition.z = transform.position.z; // Match the line's z position

        //_truck.position = newPosition; // Move the truck to the new position


     /*   Vector3 myPosition = transform.position;
        Debug.Log("Truck position: " + myPosition);
        Vector3 targetPosition = new Vector3(myPosition.x, myPosition.y, myPosition.z + _spaceBetweenLines);*/
        _truck.DOMove(newPosition, 2f).SetEase(Ease.OutBack);
    }
}

