using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    [SerializeField]
    private bool hasBeenClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        // Idle animation
    }

    // Update is called once per frame
    void Update()
    {
        //handleMovement();
    }
}

/* private void handleMovement()
 {

     if (hasBeenClicked)
     {
         // Check for user input (tap)
         if (Input.GetMouseButtonDown(1)) // You can also use Input.touchCount > 0 for touch devices
         {
             
         }
     }
 }

 void OnMouseDown()
 {
     hasBeenClicked = true;
 }
}*/
