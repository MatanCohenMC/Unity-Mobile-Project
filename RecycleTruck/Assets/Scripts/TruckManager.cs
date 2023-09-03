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
        GameManager.Instance.OnGameSetup += SetupTruck;
        getBodyRendere();
    }

    private void Start()
    {
        m_HealthManager = GameManager.Instance.GetComponent<HealthManager>();
    }


    private void Update()
    {
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
        Debug.Log($"Truck set to init position: {transform.position}");
        initializeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger")
        {
            m_SpawnManager.SpawnTriggerEntered();
        }
        else if (other.gameObject.tag.Contains("ObjectToCollect"))
        {
            Debug.Log("Player hit an object");

            Debug.Log(m_CurrentColor.ToString());
            removeObjectAfterCollisionWithTruck(other);

            if ((other.gameObject.tag.Contains(m_CurrentColor.ToString())))
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
        m_BodyRenderer = transform.Find("garbageTruck").Find("body")?.GetComponent<MeshRenderer>();
        if (m_BodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
        }
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

    private void changeToRandomColor()
    {
        if (m_BodyRenderer == null)
        {
            Debug.LogError("Body MeshRenderer not found!");
            return;
        }

        // Change to a random state (excluding the current state)
        TruckColor newTruckColor = m_CurrentColor;
        while (newTruckColor == m_CurrentColor)
        {
            newTruckColor = (TruckColor)Random.Range(0, 4);
        }

        m_CurrentColor = newTruckColor;

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
}


