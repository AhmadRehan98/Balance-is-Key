using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CalcSmoothRotation : MonoBehaviour
{
    public Transform p1, p2;
    
    public float timeToReachTarget;

    public float maxRotateSpeed;
    private float t = 0;
    private float camVelocity = 0;
    private void FixedUpdate()
    {
        Vector3 dir = p2.position - p1.position;
        Vector2 planarVec = new Vector2(dir.x, dir.z);
        Vector2 perpVec = Vector2.Perpendicular(planarVec);
        Quaternion newAngle = Quaternion.LookRotation(new Vector3(perpVec.x, 0, perpVec.y));
        
        t = Mathf.SmoothDamp(t, 1, ref camVelocity, timeToReachTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, newAngle, t);
        transform.position = Vector3.Lerp(p1.position, p2.position, 0.5f);
        float angleDiff = Quaternion.Angle(transform.rotation, newAngle);
        if (Mathf.Approximately(angleDiff, 0))
            t = 0;
    }
}
