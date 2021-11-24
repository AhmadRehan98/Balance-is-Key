using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EvenlySpaceSelected : MonoBehaviour
{
    [MenuItem("Tools/Evenly Space Selected Objects")]
    static void DistributeObjects()
    {
        Vector3 maxDist = Vector3.zero;
        foreach (GameObject curr in Selection.gameObjects)
        {
            foreach (GameObject other in Selection.gameObjects)
            {
                Vector3 currDistanceVec = curr.transform.position - other.transform.position;
                if (currDistanceVec.magnitude > maxDist.magnitude)
                {
                    maxDist = currDistanceVec;
                }
            }    
        }

        foreach (GameObject o in Selection.gameObjects)
        {
            o.transform.position = Vector3.Project(o.transform.position, maxDist);
        }
    }
}
