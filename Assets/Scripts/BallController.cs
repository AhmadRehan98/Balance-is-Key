using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float gravityScale = 1.0f;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false; // disable the engine's gravity so we can apply our own
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        _rb.AddForce(gravityScale * Physics.gravity * _rb.mass, ForceMode.Force);
    }
}
