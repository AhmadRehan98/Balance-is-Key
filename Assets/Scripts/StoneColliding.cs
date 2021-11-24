using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneColliding : MonoBehaviour
{
    private SoundManager sm;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" || other.collider.tag == "Platform")
        {
            Debug.Log("Colliding with:" + other.collider.tag);
            sm.PlayFootStepsStone();
        }
    }
}
