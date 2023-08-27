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
    private enum TruckColor { Green, Blue, Orange, Purple }
    private TruckColor _currentColor;
    private float _nextChangeTime;

    public Material[] _bodyMaterials; // An array of materials to use for the body
    private MeshRenderer _bodyRenderer; // Reference to the MeshRenderer of the body


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        // Set the initial state and schedule the first state change after a random delay
        _currentColor = TruckColor.Green;
        _nextChangeTime = Time.time + Random.Range(5f, 10f);

        // Find the MeshRenderer component of the body
        _bodyRenderer = transform.Find("body")?.GetComponent<MeshRenderer>();

        if (_bodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
        }
    }

    private void Update()
    {
        if (_gameManager.CurrentGameState == GameState.Playing && Time.time >= _nextChangeTime)
        {
            // Change to a random state (excluding the current state)
            TruckColor newTruckColor = _currentColor;
            while (newTruckColor == _currentColor)
            {
                newTruckColor = (TruckColor)Random.Range(0, 4);
            }

            _currentColor = newTruckColor;

            // TODO: changeColor() - Change the color of the truck
            changeColor(newTruckColor);

            Debug.Log($"Truck color changed to {_currentColor}");

            // Calculate the next random interval between 5 and 10 seconds
            float randomInterval = Random.Range(5f, 10f);
            Debug.Log($"for {randomInterval} seconds");

            // Set the next change time
            _nextChangeTime = Time.time + randomInterval;
        }
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

    private void changeColor(TruckColor newTruckColor)
    {
        if (_bodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
            return;
        }

        // Ensure there's at least one material assigned to the body
        if (_bodyMaterials.Length > 0)
        {
            // Assign the material of Element 0
            //_bodyRenderer.materials[0].color = Color.blue;
           /* _bodyRenderer.materials[0] = _bodyMaterials[(int)newTruckColor];
            Debug.Log($"newTruckColor: {newTruckColor}");
            Debug.Log($"_bodyMaterials[(int)newTruckColor]: {_bodyMaterials[(int)newTruckColor]}");
            Debug.Log($"_bodyRenderer.materials[0]: {_bodyRenderer.materials[0]}");*/


            // Get the current materials array
            Material[] currentMaterials = _bodyRenderer.materials;

            // Change the first element's material
            currentMaterials[0] = _bodyMaterials[(int)newTruckColor];

            // Assign the modified materials array back to the body's MeshRenderer
            _bodyRenderer.materials = currentMaterials;
        }
        else
        {
            Debug.LogWarning("No body materials assigned.");
        }
    }
}


