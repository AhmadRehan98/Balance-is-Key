using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script to controll a player with a rigidbody component
public class RigidbodyController : MonoBehaviour
{
    public float Acceleration = 50f; // net force increase per FixedUpdate call
    public float TurnSpeed = 2f; // turn speed in radians/second

    [Range(1, 4)] // lets you restrict the player number in the unity editor
    public int player = 1;

    private string _hAxis;
    private string _vAxis;
    private Rigidbody _rb;
    private Rigidbody _platform;
    private LineRenderer[] _lines;
    private SpringJoint[] _springs;

    private Vector3 _input = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _hAxis = "p" + player + "Horizontal";
        _vAxis = "p" + player + "Vertical";
        
        _rb = GetComponent<Rigidbody>();
        // _springs = GetComponents<SpringJoint>();
        // _lines = transform.parent.GetComponentsInChildren<LineRenderer>();
        // print(_lines.Length);
        // _platform = _springs[0].connectedBody;
    }

    void Update()
    {
        _input = Vector3.zero;
        _input.x = Input.GetAxis(_hAxis);
        _input.z = Input.GetAxis(_vAxis);

        // if (player == 1)
        //     _input.y = Input.GetAxis("leftTrigger");
        // else if (player == 2)
        //     _input.y = Input.GetAxis("rightTrigger");

        // for (int i = 0; i < _lines.Length; i++)
        // {
        //     Vector3[] pos = {_springs[i].anchor, _springs[i].connectedAnchor};
        //     _lines[i].SetPositions(pos);
        // }
    }

    // FixedUpdate is not affected by framerate, it is called at a fixed time interval so may be called 0, 1, or more times per depending on the current framerate
    void FixedUpdate()
    {
        _rb.AddForce(_input * Acceleration,
            ForceMode.Force); // adjust the net force vector every frame by adding a force in the direction of the user input
    }
}