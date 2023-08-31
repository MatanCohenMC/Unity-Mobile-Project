using UnityEngine;

public enum TruckColor { Brown, Blue, Orange, Purple }

public class TruckManager : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Vector3 _initPosition;
    private TruckColor _currentColor;
    private float _nextChangeTime;
    public Material[] _bodyMaterials;
    private MeshRenderer _bodyRenderer; 


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        transform.position = _initPosition;
        Debug.Log($"Truck set to init position: {transform.position}");
        getBodyRendere();
        initializeColor();
    }

    private void Update()
    {
        if (_gameManager.CurrentGameState == GameState.Playing && Time.time >= _nextChangeTime)
        {
            changeToRandomColor();

            // Calculate the next random interval between 7 and 15 seconds and set the next change time
            _nextChangeTime = Time.time + Random.Range(7f, 15f);

            Debug.Log($"Truck color changed to {_currentColor}.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _spawnManager.SpawnTriggerEntered();
    }

    private void getBodyRendere()
    {
        // Find the MeshRenderer component of the body
        _bodyRenderer = transform.Find("garbageTruck").Find("body")?.GetComponent<MeshRenderer>();
        if (_bodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
        }
    }

    private void initializeColor()
    {
        // Set the initial state and schedule the first state change after a random delay
        _currentColor = TruckColor.Brown;
        _nextChangeTime = Time.time + 10f;
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


