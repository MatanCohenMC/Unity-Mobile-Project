using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StripSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _strips;
    private float _offset = 24f; // 24
    private float _stripsX = -100f;
    private float _stripsY = -10f;

    void Start()
    {
        if(_strips is { Count: > 0 })
        {
            _strips = _strips.OrderBy(strip => strip.transform.position.z).ToList();
        }
    }

    public void MoveStrip()
    {
        GameObject movedStrip = _strips[0];
        _strips.Remove(movedStrip);
        float newZ = _strips[^1].transform.position.z + _offset;
        movedStrip.transform.position = new Vector3(_stripsX, _stripsY, newZ);
        _strips.Add(movedStrip);
    }
}
