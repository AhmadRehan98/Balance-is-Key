using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float gravityScale = 1.0f;
    public Vector3 centerOfMassOffset = Vector3.zero;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false; // disable the engine's gravity so we can apply our own
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        _rb.AddForceAtPosition(gravityScale * Physics.gravity * _rb.mass, transform.TransformPoint(centerOfMassOffset), ForceMode.Force);
        // _rb.AddForce(gravityScale * Physics.gravity * _rb.mass, ForceMode.Force);
    }
}
