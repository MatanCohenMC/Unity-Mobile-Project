using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectsSpawner : MonoBehaviour
{
    public const int m_NumOfLanes = 3;
    public GameObject[] m_ObjectsToCollect;
    public GameObject[] m_Strips;

    private void Start()
    {
        int randomObjectIndex;
        int randomLaneIndex;      

        foreach (GameObject strip in m_Strips)
        {

            // get not active random object
            do
            {
                randomObjectIndex = Random.Range(0, m_ObjectsToCollect.Length);
            } 
            while (m_ObjectsToCollect[randomObjectIndex].activeSelf);

            randomLaneIndex = Random.Range(0, m_NumOfLanes);

            Transform randomLaneTransform = strip.transform.Find("ForwardStrip").Find("3LaneRoad").GetChild(randomLaneIndex);

            // spawn random object on strip
            m_ObjectsToCollect[randomObjectIndex].SetActive(true);
            m_ObjectsToCollect[randomObjectIndex].transform.SetPositionAndRotation(randomLaneTransform.position, Quaternion.identity);
            m_ObjectsToCollect[randomObjectIndex].transform.SetParent(strip.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //int randomIndex = Random.Range(0, m_ObjectsToCollect.Length);
        //Vector3 randomSpawnPosition = new Vector3(Random.Range()
    }
}
