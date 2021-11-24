using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class IsMoving : MonoBehaviour
{
    private SoundManager sm;
    private Vector3 lastPos;
    // private float velocitySensetivity = 0.000001f;
    private float positionSensetivity = 0.18f;
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
        
    }

    private void OnCollisionStay(Collision other)
    {
        float x_movement = Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.x - lastPos.x);
        float z_movement = Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.z - lastPos.z);
        // if (x_movement > positionSensetivity)
        //     Debug.Log("x: " + x_movement + " sens: " + positionSensetivity);
        // if (z_movement > positionSensetivity)
        //     Debug.Log("z: " + z_movement + " sens: " + positionSensetivity);

        if (x_movement > positionSensetivity || z_movement > positionSensetivity)
        {
            if (other.collider.tag == "Ground")
                sm.PlayFootStepsStone();
            else if (other.collider.tag == "Dirt")
                sm.PlayFootStepsDirt();
        }
        // Debug.Log("Colliding with:" + other.collider.tag + " at speed: " + 
        //           Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.y - lastPos.y));
        // if (Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.y - lastPos.y) < 0.0001f)
        // {
        //     return;
        // }
        // if ((gameObject.GetComponent<Rigidbody>().velocity.x > velocitySensetivity  ||
        //      gameObject.GetComponent<Rigidbody>().velocity.z > velocitySensetivity) &&
        //     (Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.x - lastPos.x) > positionSensetivity ||
        //      Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.z - lastPos.z) > positionSensetivity ))
        // {
        //     if (other.collider.tag == "Ground")
        //         sm.PlayFootStepsStone();
        //     else if (other.collider.tag == "Dirt")
        //         sm.PlayFootStepsDirt();
        // }
    
        lastPos = new Vector3(gameObject.GetComponent<Rigidbody>().transform.position.x,
            gameObject.GetComponent<Rigidbody>().transform.position.y,
            gameObject.GetComponent<Rigidbody>().transform.position.z);
        
    }
}
