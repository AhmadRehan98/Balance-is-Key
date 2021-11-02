using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class IsMoving : MonoBehaviour
{
    private SoundManager sm;
    private Vector3 lastPos;
    public float velocitySensetivity = 0.000001f;
    public float positionSensetivity = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        lastPos = gameObject.GetComponent<Rigidbody>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.y - lastPos.y) > 0.1f)
        {
            return;
            // TODO: should also get tag of any ground surface, if you're not touching it, no foot steps get played.
        }
        if ((gameObject.GetComponent<Rigidbody>().velocity.x > velocitySensetivity  ||
             gameObject.GetComponent<Rigidbody>().velocity.z > velocitySensetivity) &&
            (Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.x - lastPos.x) > positionSensetivity ||
             Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.z - lastPos.z) > positionSensetivity ))
        {
            sm.PlayFootStep();
        }
        
        lastPos = new Vector3(gameObject.GetComponent<Rigidbody>().transform.position.x,
            gameObject.GetComponent<Rigidbody>().transform.position.y,
            gameObject.GetComponent<Rigidbody>().transform.position.z);
    }
}
