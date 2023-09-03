using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private StripSpawner m_StripSpawner;

    void Start()
    {
        m_StripSpawner = GetComponent<StripSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        m_StripSpawner.MoveStrip();
    }
}
