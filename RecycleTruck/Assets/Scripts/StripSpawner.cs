using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StripSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_Strips;
    private float m_Offset = 24.14f;
    private float m_StripsX = 0f;
    private float m_StripsY = 100f;
    public const int m_NumOfLanes = 3;
    public GameObject[] m_ObjectsToCollect;

    void Start()
    {
        // ordering strips by ascending order
        if (m_Strips is { Count: > 0 })
        {
            m_Strips = m_Strips.OrderBy(strip => strip.transform.position.z).ToList();
        }

        // spawning random object on strips
        for (int i = 1; i < m_Strips.Count; i++)
        {
            spawnRandomObjectOnRandomLaneOfStrip(m_Strips[i]);
        }
    }

    // this method moves strip to the end of the road.
    public void MoveStrip()
    {
        GameObject movedStrip = m_Strips[0];
        RemoveObjectFromStrip(movedStrip.transform);
        m_Strips.Remove(movedStrip);
        float newZ = m_Strips[^1].transform.position.z + m_Offset;
        movedStrip.transform.position = new Vector3(m_StripsX, m_StripsY, newZ);
        spawnRandomObjectOnRandomLaneOfStrip(movedStrip);
        m_Strips.Add(movedStrip);
    }

    // this method removes the random object from the i_StripTransform
    public void RemoveObjectFromStrip(Transform i_StripTransform)
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
    }

    // this method spawns random object on random lane of strip
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
