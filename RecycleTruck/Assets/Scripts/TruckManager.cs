using UnityEngine;

public enum TruckColor { Brown, Blue, Orange, Purple }

public class TruckManager : MonoBehaviour
{
    [SerializeField] private SpawnManager m_SpawnManager;
    [SerializeField] private Vector3 m_InitPosition;
    private TruckColor m_CurrentColor;
    private float m_NextChangeTime;
    public Material[] m_BodyMaterials;
    private MeshRenderer m_BodyRenderer;
    private HealthManager m_HealthManager;

    private void Awake()
    {
        // Subscribe the SetupTruck method to the OnGameSetup event in the GameManager instance.
        GameManager.Instance.OnGameSetup += SetupTruck;
        getBodyRendere();
    }

    private void Start()
    {
        getMembersComponents();
    }

    private void getMembersComponents()
    {
        m_HealthManager = GameManager.Instance.GetComponent<HealthManager>();
    }

    private void Update()
    {
        // if the game is in 'Playing' state and if it's time to change the truck's color.
        if (GameManager.Instance.CurrentGameState == GameState.Playing && Time.time >= m_NextChangeTime)
        {
            changeToRandomColor();
            // Calculate the next random interval between 7 and 15 seconds and set the next change time
            m_NextChangeTime = Time.time + Random.Range(7f, 15f);
            Debug.Log($"Truck color changed to {m_CurrentColor}.");
        }
    }

    // this method setups the truck's position and its color to its initial position and color.
    public void SetupTruck()
    {
        transform.position = m_InitPosition;
        initializeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {
            m_SpawnManager.SpawnTriggerEntered();
        }
        else if (isPlayerHitObjectToCollect(other)) 
        {
            playerHitObjectToCollect(other);
        }
    }

    // this method handles when the player collides with an object to collect
    private void playerHitObjectToCollect(Collider other)
    {
        Debug.Log("Player hit an object to collect");
        removeObjectAfterCollisionWithTruck(other);
        if (isPlayerHitObjectMatchingTruckColor(other))
        {
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddPointsToPlayerScore();
        }
        else
        {            
            playerHitWrongObject();
        }
    }

    // Player hit wrong object, decrease lives and check if game over
    private void playerHitWrongObject()
    {
        m_HealthManager.DecreaseOrIncreaseHeartAmount(false);
        if (m_HealthManager.HealthAmountRemain <= 0)
        {
            GameManager.Instance.EndGame();
        }
    }

    private bool isPlayerHitObjectMatchingTruckColor(Collider other)
    {
        return other.gameObject.tag.Contains(m_CurrentColor.ToString());
    }

    private bool isPlayerHitObjectToCollect(Collider other)
    {
        return other.gameObject.tag.Contains("ObjectToCollect");
    }

    // this method removes object from its strip parent after collision with the truck.
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
            Debug.LogError("StripSpawner script component not found on the GameObject.");
        }
    }

    private void getBodyRendere()
    {
        // Find the MeshRenderer component of the body
        m_BodyRenderer = transform.Find("garbageTruck").Find("body")?.GetComponent<MeshRenderer>();
        checkIfBodyRendererNull();
    }

    private void initializeColor()
    {
        // Set the initial state and schedule the first state change after a random delay
        m_CurrentColor = TruckColor.Brown;
        Material[] currentMaterials = m_BodyRenderer.materials;
        // Change the first element's material
        currentMaterials[0] = m_BodyMaterials[(int)m_CurrentColor];
        // Assign the modified materials array back to the body's MeshRenderer
        m_BodyRenderer.materials = currentMaterials;
        m_NextChangeTime = Time.time + 10f;
    }

    // this method changes the truck's color to a different random color.
    private void changeToRandomColor()
    {
        if(!checkIfBodyRendererNull())
        {
            m_CurrentColor = findNewRandomColorForTruck();
            setTruckColor();
        }    
    }

    // this method sets truck color:
    private void setTruckColor()
    {
        // Ensure there's at least one material assigned to the body
        if (m_BodyMaterials.Length > 0)
        {
            // Get the current materials array
            Material[] currentMaterials = m_BodyRenderer.materials;
            // Change the first element's material
            currentMaterials[0] = m_BodyMaterials[(int)m_CurrentColor];
            // Assign the modified materials array back to the body's MeshRenderer
            m_BodyRenderer.materials = currentMaterials;
        }
        else
        {
            Debug.LogWarning("No body materials assigned.");
        }
    }

    private bool checkIfBodyRendererNull()
    {
        bool res = false;

        if (m_BodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
            res = true;
        }

        return res;
    }

    // this method finds the next random color for truck that is different than the current color of the truck.
    private TruckColor findNewRandomColorForTruck()
    {
        TruckColor newTruckColor;

        do
        {
            newTruckColor = (TruckColor)Random.Range(0, 4);
        }
        while (newTruckColor == m_CurrentColor);

        return newTruckColor;
    }
}


