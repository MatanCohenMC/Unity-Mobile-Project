using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StripSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _strips;
    private float _offset = 24.14f;
    private float _stripsX = 0f;
    private float _stripsY = 100f;
    public const int m_NumOfLanes = 3;
    public GameObject[] m_ObjectsToCollect;

    void Start()
    {


        if (_strips is { Count: > 0 })
        {
            _strips = _strips.OrderBy(strip => strip.transform.position.z).ToList();
        }

        for (int i = 1; i < _strips.Count; i++)
        {
            spawnRandomObjectOnRandomLaneOfStrip(_strips[i]);
        }
    }

    public void MoveStrip()
    {
        GameObject movedStrip = _strips[0];
        removeObjectFromStrip(movedStrip.transform);
        _strips.Remove(movedStrip);
        float newZ = _strips[^1].transform.position.z + _offset;
        movedStrip.transform.position = new Vector3(_stripsX, _stripsY, newZ);
        spawnRandomObjectOnRandomLaneOfStrip(movedStrip);
        _strips.Add(movedStrip);
    }

    private void removeObjectFromStrip(Transform i_StripTransform)
    {
        // the transform of the object on the strip we want to remove.
        Transform transfromOfObject = null;

        foreach (Transform child in i_StripTransform)
        {
            if (child.name.Contains("ToCollect"))
            {
                transfromOfObject = child;
                transfromOfObject.gameObject.SetActive(false);
                transfromOfObject.SetParent(GameObject.Find("ObjectsToCollectByPlayer").transform);
                break;
            }
        }

        

        

        //int childCount = i_StripTransform.childCount;
        //for (int i = 0; i < childCount; i++)
        //{
        //    Transform childTransform = i_StripTransform.GetChild(i);
        //    if (childTransform.name.Contains("ToCollect"))
        //    {
        //        transfromOfObject = childTransform;
        //        break;
        //    }
        //}
    }

    private void spawnRandomObjectOnRandomLaneOfStrip(GameObject i_Strip)
    {
        int randomObjectIndex;
        int randomLaneIndex;

        // get not active random object
        do
        {
            randomObjectIndex = Random.Range(0, m_ObjectsToCollect.Length);
        }
        while (m_ObjectsToCollect[randomObjectIndex].activeSelf);

        // get random number for index lane
        randomLaneIndex = Random.Range(0, m_NumOfLanes);

        // find the transform of the lane in the randomLaneIndex
        Transform randomLaneTransform = i_Strip.transform.Find("ForwardStrip").Find("3LaneRoad").GetChild(randomLaneIndex);

        // spawn random object on strip
        m_ObjectsToCollect[randomObjectIndex].SetActive(true);
        m_ObjectsToCollect[randomObjectIndex].transform.SetPositionAndRotation(randomLaneTransform.position, Quaternion.identity);
        m_ObjectsToCollect[randomObjectIndex].transform.SetParent(i_Strip.transform);
    }
}
