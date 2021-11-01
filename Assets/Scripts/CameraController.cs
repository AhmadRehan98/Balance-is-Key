using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform[] followObjects;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _offset = CalcAveragePos() - transform.position;
    }

    // returns the average position of every element of followObjects
    Vector3 CalcAveragePos()
    {
        if (followObjects.Length == 0) return Vector3.zero;
        
        Vector3 avg = Vector3.zero;
        foreach (Transform t in followObjects)
        {
            avg += t.position;
        }

        avg /= followObjects.Length;
        return avg;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 avg = CalcAveragePos();
        transform.position = avg - _offset;
    }
}
