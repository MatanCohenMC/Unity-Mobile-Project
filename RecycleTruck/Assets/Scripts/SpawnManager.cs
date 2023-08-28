using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private StripSpawner _stripSpawner;

    void Start()
    {
        _stripSpawner = GetComponent<StripSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        _stripSpawner.MoveStrip();
    }
}
