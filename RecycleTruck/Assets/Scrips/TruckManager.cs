using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine;

public class TruckManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Vector3 _initPosition;
    private enum TruckColor { Green, Blue, Orange, Purple }
    private TruckColor _currentColor;
    private float _nextChangeTime;

    public Material[] _bodyMaterials;
    private MeshRenderer _bodyRenderer; 


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        initializeColor();
        
    }

    private void Update()
    {
        if (_gameManager.CurrentGameState == GameState.Playing && Time.time >= _nextChangeTime)
        {
            changeToRandomColor();

            // Calculate the next random interval between 5 and 10 seconds and set the next change time
            _nextChangeTime = Time.time + Random.Range(5f, 10f);

            Debug.Log($"Truck color changed to {_currentColor}.");
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

    private void initializeColor()
    {
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

    private void changeToRandomColor()
    {
        if (_bodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
            return;
        }

        // Change to a random state (excluding the current state)
        TruckColor newTruckColor = _currentColor;
        while (newTruckColor == _currentColor)
        {
            newTruckColor = (TruckColor)Random.Range(0, 4);
        }

        _currentColor = newTruckColor;

        // Ensure there's at least one material assigned to the body
        if (_bodyMaterials.Length > 0)
        {
            // Get the current materials array
            Material[] currentMaterials = _bodyRenderer.materials;

            // Change the first element's material
            currentMaterials[0] = _bodyMaterials[(int)_currentColor];

            // Assign the modified materials array back to the body's MeshRenderer
            _bodyRenderer.materials = currentMaterials;
        }
        else
        {
            Debug.LogWarning("No body materials assigned.");
        }
    }
}


