using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    private SoundManager sm;
    private float velocitySensetivity = 0.5f;

    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        sm.PauseBallRolling();
    }


    private void OnCollisionStay(Collision other)
    {
        //Debug.Log(other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude > velocitySensetivity)
        {
            sm.PlayBallRolling(other.relativeVelocity.magnitude * 0.5f);
        }
        else
        {
            sm.PauseBallRolling();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        sm.PauseBallRolling();
        //Debug.Log("exited collision");
    }

    
}
