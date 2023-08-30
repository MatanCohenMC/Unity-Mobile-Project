//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RandomObjectsSpawner : MonoBehaviour
//{
//    public const int m_NumOfLanes = 3;
//    public static GameObject[] m_ObjectsToCollect;
//    public GameObject[] m_Strips;

//    private void Start()
//    {

//        foreach (GameObject strip in m_Strips)
//        {
//            spawnRandomObjectOnRandomLaneOfStrip(strip);
//        }
//    }


//    public static void removeObjectFromStrip(Transform i_StripTransform)
//    {
//        // the transform of the object on the strip we want to remove.
//        Transform transfromOfObject = null;

//        foreach (Transform child in i_StripTransform)
//        {
//            if (child.name.Contains("ToCollect"))
//            {
//                transfromOfObject = child;
//                break;
//            }
//        }

//        transfromOfObject.gameObject.SetActive(false);

//        //int childCount = i_StripTransform.childCount;
//        //for (int i = 0; i < childCount; i++)
//        //{
//        //    Transform childTransform = i_StripTransform.GetChild(i);
//        //    if (childTransform.name.Contains("ToCollect"))
//        //    {
//        //        transfromOfObject = childTransform;
//        //        break;
//        //    }
//        //}
//    }

//    public static void spawnRandomObjectOnRandomLaneOfStrip(GameObject i_Strip)
//    {
//        int randomObjectIndex;
//        int randomLaneIndex;

//        // get not active random object
//        do
//        {
//            randomObjectIndex = Random.Range(0, m_ObjectsToCollect.Length);
//        }
//        while (m_ObjectsToCollect[randomObjectIndex].activeSelf);

//        // get random number for index lane
//        randomLaneIndex = Random.Range(0, m_NumOfLanes);

//        // find the transform of the lane in the randomLaneIndex
//        Transform randomLaneTransform = i_Strip.transform.Find("ForwardStrip").Find("3LaneRoad").GetChild(randomLaneIndex);

//        // spawn random object on strip
//        m_ObjectsToCollect[randomObjectIndex].SetActive(true);
//        m_ObjectsToCollect[randomObjectIndex].transform.SetPositionAndRotation(randomLaneTransform.position, Quaternion.identity);
//        m_ObjectsToCollect[randomObjectIndex].transform.SetParent(i_Strip.transform);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //int randomIndex = Random.Range(0, m_ObjectsToCollect.Length);
//        //Vector3 randomSpawnPosition = new Vector3(Random.Range()
//    }
//}
