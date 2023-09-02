using UnityEngine;

public enum TruckColor { Brown, Blue, Orange, Purple }

public class TruckManager : MonoBehaviour
{
    //private GameManager _gameManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private Vector3 _initPosition;
    private TruckColor _currentColor;
    private float _nextChangeTime;
    public Material[] _bodyMaterials;
    private MeshRenderer _bodyRenderer;
    private HealthManager m_HealthManager;



    private void Awake()
    {
        //_gameManager = FindObjectOfType<GameManager>();
        GameManager.Instance.OnGameSetup += SetupTruck;
        //ResetTruck();
        getBodyRendere();
    }

    private void Start()
    {
        m_HealthManager = GameManager.Instance.GetComponent<HealthManager>();
    }


    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameState.Playing && Time.time >= _nextChangeTime)
        {
            changeToRandomColor();

            // Calculate the next random interval between 7 and 15 seconds and set the next change time
            _nextChangeTime = Time.time + Random.Range(7f, 15f);

            Debug.Log($"Truck color changed to {_currentColor}.");
        }
    }

    public void SetupTruck()
    {
        transform.position = _initPosition;
        Debug.Log($"Truck set to init position: {transform.position}");
        initializeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {
            _spawnManager.SpawnTriggerEntered();
        }
        else if (other.gameObject.tag.Contains("ObjectToCollect"))
        {
            Debug.Log("Player hit an object");

            Debug.Log(_currentColor.ToString());
            removeObjectAfterCollisionWithTruck(other);

            if ((other.gameObject.tag.Contains(_currentColor.ToString())))
            {
                //ScoreManager.Instance.AddPointsToPlayerScore();
                GameManager.Instance.GetComponent<ScoreManager>().AddPointsToPlayerScore();
            }
            else
            {
                // Player hit wrong object, decrease lives and check if game over
                m_HealthManager.DecreaseOrIncreaseHeartAmount(false);

                if (m_HealthManager.HealthAmountRemain <= 0)
                {
                    GameManager.Instance.EndGame();
                }
            }
            

        }

    }


    private void removeObjectAfterCollisionWithTruck(Collider other)
    {
        // Find the GameObject with the "StripSpawner" script component
        GameObject gameObjectWhichIncludesStripSpawnerScript = GameObject.Find("SpawnManager");

        // Access the "StripSpawner" script component
        StripSpawner stripSpawnerComponent = gameObjectWhichIncludesStripSpawnerScript.GetComponent<StripSpawner>();

        // Check if the script component is found
        if (stripSpawnerComponent != null)
        {
            // Call the RemoveObjectFromStrip method in "StripSpawner"
            stripSpawnerComponent.RemoveObjectFromStrip(other.transform.parent);
        }
        else
        {
            Debug.LogError("Func2 script component not found on the GameObject.");
        }
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
        Debug.LogError("IM HERE (:::::::");

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


