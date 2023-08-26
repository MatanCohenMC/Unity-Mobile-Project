using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    StripSpawner _stripSpawner;

    void Start()
    {
        _stripSpawner = GetComponent<StripSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        _stripSpawner.MoveStrip();
    }
}
