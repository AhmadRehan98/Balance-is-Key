using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform[] players;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // returns the average position of every element of followObjects
    Vector3 CalcAveragePos()
    {
        if (players.Length == 0) return Vector3.zero;
        
        Vector3 avg = Vector3.zero;
        foreach (Transform t in players)
        {
            avg += t.position;
        }

        avg /= players.Length;
        return avg;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.MovePosition(CalcAveragePos());
        
    }
}
