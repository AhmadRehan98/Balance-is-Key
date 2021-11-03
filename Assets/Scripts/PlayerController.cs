using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Range(1, 2)] // only support 2 players for now
    public int player = 1;

    // hand parameters
    public float handMaxDistance; // min and max distance hand can travel
    public float handSpeed; // speed hands move at
    public float handDampeningCoeff = 0.5f; // 0 == no dampening, 1 = infinite dampening (ie no movement)

    // body parameters
    public float bodyAcceleration = 50f; // net force increase per FixedUpdate call


    // rigid bodies
    private Rigidbody _bodyRb;
    public Rigidbody handRbL, handRbR;

    // hand private vars
    private Vector3 _handMinVec, _handMaxVec; // based on handMinDistance and handMaxDistance. For use with Vector3.Lerp()
    private Vector3 _handTargetVecL, _handTargetVecR; // desired position of hands based on user input
    private Vector3 _handMoveDirL, _handMoveDirR; // direction and magnitude to move hand in to reach desired position
    private Vector3 _handStartingPosL, _handStartingPosR; // local starting position of hands

    // body input vector
    private Vector3 _moveInput = Vector3.zero;

    // starting local positions for checkpoint system
    private List<Vector3> _startingTransformPositions;

    // camera transform for relative inputs
    private Transform _camTransform;

    void Start()
    {
        // assign component references
        _bodyRb = GetComponent<Rigidbody>();
        print(_bodyRb.name);
        Rigidbody[] childRbs = GetComponentsInChildren<Rigidbody>();

        if (!Mathf.Approximately(handRbL.position.y, handRbR.position.y))
            Debug.LogWarning("Hands for " + transform.name + " have different starting heights");


        _handMinVec = new Vector3(0, handRbL.transform.localPosition.y, 0);
        _handMaxVec = new Vector3(0, handMaxDistance, 0);

        _handStartingPosL = handRbL ? new Vector3(handRbL.transform.localPosition.x, 0, handRbL.transform.localPosition.z) : Vector3.zero;
        _handStartingPosR = handRbR ? new Vector3(handRbR.transform.localPosition.x, 0, handRbR.transform.localPosition.z) : Vector3.zero;
        _handTargetVecL = _handMinVec;
        _handTargetVecR = _handMinVec;

        // store local positions of all transforms in the player setup, and their children (if any)
        _startingTransformPositions = new List<Vector3>();
        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            _startingTransformPositions.Add(transform.parent.parent.GetChild(i).transform.localPosition);
            for (int j = 0; j < transform.parent.parent.GetChild(i).childCount; j++)
            {
                _startingTransformPositions.Add(transform.parent.parent.GetChild(i).GetChild(j).transform.localPosition);
            }
        }
        
        if (Camera.main is { }) _camTransform = Camera.main.transform;
    }


    void FixedUpdate()
    {
        // calculate force vector to go in direction of target hand position and apply impulse force each frame
        if (handRbL)
        {
            Vector3 yPos = new Vector3(0, handRbL.transform.localPosition.y, 0);
            Vector3 dampVec = new Vector3(0, handRbL.velocity.y * handDampeningCoeff, 0);
            _handMoveDirL = _handTargetVecL - yPos;
            handRbL.AddForce(_handMoveDirL * handSpeed - dampVec, ForceMode.VelocityChange);
        }

        if (handRbR)
        {
            Vector3 yPos = new Vector3(0, handRbR.transform.localPosition.y, 0);
            Vector3 dampVec = new Vector3(0, handRbR.velocity.y * handDampeningCoeff, 0);
            _handMoveDirR = _handTargetVecR - yPos;
            handRbR.AddForce(_handMoveDirR * handSpeed - dampVec, ForceMode.VelocityChange);
        }

        // for player movement, adjust the net force vector every frame by adding a force in the direction of the user input
        _bodyRb.AddForce(_moveInput * bodyAcceleration, ForceMode.Force);

        // print(_moveInput * bodyAcceleration);
        // print(transform.name + " " + _handMoveDirL);
    }

    public void OnPlayerMove(InputAction.CallbackContext input)
    {
        if (_camTransform != null)
        {
            Vector2 inVec = input.ReadValue<Vector2>();

            Vector3 forward = _camTransform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = _camTransform.right;
            right.y = 0;
            right.Normalize();

            _moveInput = inVec.y * forward + inVec.x * right;
        }
        else
        {
            Vector2 inVec = input.ReadValue<Vector2>();
            _moveInput.x = inVec.x;
            _moveInput.y = 0;
            _moveInput.z = inVec.y;
        }
    }

    public void OnLeftArm(InputAction.CallbackContext input)
    {
        _handTargetVecL = Vector3.Lerp(_handMinVec, _handMaxVec, input.ReadValue<float>());
    }

    public void OnRightArm(InputAction.CallbackContext input)
    {
        _handTargetVecR = Vector3.Lerp(_handMinVec, _handMaxVec, input.ReadValue<float>());
    }

    public void OnResetScene(InputAction.CallbackContext input)
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        int currTransformIdx = 0;
        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            transform.parent.parent.GetChild(i).transform.localPosition = _startingTransformPositions[currTransformIdx];
            currTransformIdx += 1;
            for (int j = 0; j < transform.parent.parent.GetChild(i).childCount; j++)
            {
                transform.parent.parent.GetChild(i).GetChild(j).transform.localPosition = _startingTransformPositions[currTransformIdx];
                currTransformIdx += 1;
            }
        }

        transform.parent.parent.position = CheckpointController.lastCheckpoint.position;
        ;
        ;
    }
}