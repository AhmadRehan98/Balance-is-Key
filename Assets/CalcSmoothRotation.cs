using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcSmoothRotation : MonoBehaviour
{
    public Transform p1, p2;
    

    private void FixedUpdate()
    {
        Vector3 dir = p2.position - p1.position;
        Vector2 planarVec = new Vector2(dir.x, dir.z);
        Vector2 perpVec = Vector2.Perpendicular(planarVec);
        
        transform.rotation = Quaternion.LookRotation(new Vector3(perpVec.x, 0, perpVec.y));
        transform.position = Vector3.Lerp(p1.position, p2.position, 0.5f);
    }
}
