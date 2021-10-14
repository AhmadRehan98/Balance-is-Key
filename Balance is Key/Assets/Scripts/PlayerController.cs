using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Range(1, 2)] // only support 2 players for now
    public int player = 1;

    // hand parameters
    public float handMinDistance, handMaxDistance;
    public float handSpeed;

    // body parameters
    public float bodyAcceleration = 50f; // net force increase per FixedUpdate call

    // rigid bodies
    private Rigidbody _handRbL, _handRbR, _bodyRb;
    
    // hand private vars
    private Vector3 _handMinVec, _handMaxVec, _handTargetVecL, _handTargetVecR;
    private Vector3 _handForceVecL, _handForceVecR;

    // input axis names
    private string _movementHorzAxis, _movementVertAxis, _leftHandAxis, _rightHandAxis;

    // body input vector
    private Vector3 _input = Vector3.zero;
    
    void Start()
    {
        _movementHorzAxis = "p" + player + "Horizontal";
        _movementVertAxis = "p" + player + "Vertical";
        _leftHandAxis = "p" + player + "LeftTrigger";
        _rightHandAxis = "p" + player + "RightTrigger";

        _bodyRb = GetComponent<Rigidbody>();
        _handRbL = GetComponentsInChildren<Rigidbody>()[1];
        _handRbR = GetComponentsInChildren<Rigidbody>()[2];
        
        
        

        _handMinVec = new Vector3(0, handMinDistance, 0);
        _handMaxVec = new Vector3(0, handMaxDistance, 0);
    }


    void FixedUpdate()
    {
        // TODO (maybe): refactor this to handle an arbitrary number of hands?

        // interpolate target position of hand based on current controller trigger value
        float tL = Input.GetAxis(_leftHandAxis);
        float tR = Input.GetAxis(_rightHandAxis);
        
        _handTargetVecL = Vector3.Lerp(_handMinVec, _handMaxVec, tL);
        _handTargetVecR = Vector3.Lerp(_handMinVec, _handMaxVec, tR);
        
        // calculate force vector to go in direction of target position
        _handForceVecL = _handTargetVecL - _handRbL.transform.localPosition + Vector3.left*3; // hard coded offset, change later
        _handForceVecR = _handTargetVecR - _handRbR.transform.localPosition + Vector3.right*3;

        print(_handForceVecL);
        print(_handForceVecR);

        
        // apply impulse force each frame
        _handRbL.AddForce(_handForceVecL * handSpeed, ForceMode.VelocityChange);
        _handRbR.AddForce(_handForceVecR * handSpeed, ForceMode.VelocityChange);

        // for player movement, adjust the net force vector every frame by adding a force in the direction of the user input
        _bodyRb.AddForce(_input * bodyAcceleration, ForceMode.Force);
    }

    void Update()
    {
        _input = Vector3.zero;
        _input.x = Input.GetAxis(_movementHorzAxis);
        _input.z = Input.GetAxis(_movementVertAxis);
        // TODO: move trigger inputs from FixedUpdate to Update? 

        if(Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}