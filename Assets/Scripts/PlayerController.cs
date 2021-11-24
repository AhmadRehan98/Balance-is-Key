using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Component references
    public Rigidbody handRbL, handRbR;
    private Rigidbody _bodyRb;
    private Transform _camTransform; // camera transform for relative inputs
    private Animator _animator;
    
    // hand parameters
    public float handMaxDistance; // min and max distance hand can travel
    public float handSpeed; // speed hands move at
    public float handDampeningCoeff = 0.5f; // 0 == no dampening, 1 = infinite dampening (ie no movement)

    // body parameters
    public float bodyAcceleration = 50f; // net force increase per FixedUpdate call

    // hand private vars
    private Vector3 _handMinVec, _handMaxVec; // based on handMinDistance and handMaxDistance. For use with Vector3.Lerp()
    private Vector3 _handTargetVecL, _handTargetVecR; // desired position of hands based on user input
    private Vector3 _handMoveDirL, _handMoveDirR; // direction and magnitude to move hand in to reach desired position
    private Vector3 _handStartingPosL, _handStartingPosR; // local starting position of hands

    // user input vector
    private Vector3 _moveInput = Vector3.zero;

    // starting local positions for checkpoint system
    private List<Vector3> _startingTransformPositions;
    
    // for toggling accessories/cosmetics
    ToggleAccessories togAcc;


    void Start()
    {
        // assign component references
        _bodyRb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.LogWarning(transform.name + "Missing AnimatorController");

        if (handRbL == null || handRbR == null)
            Debug.LogWarning("Hands for " + transform.name + " are missing");
        else if (!Mathf.Approximately(handRbL.position.y, handRbR.position.y))
            Debug.LogWarning("Hands for " + transform.name + " have different starting heights");
        else
        {
            _handMinVec = new Vector3(0, handRbL.transform.localPosition.y, 0);
            _handMaxVec = new Vector3(0, handMaxDistance, 0);

            _handStartingPosL = handRbL ? new Vector3(handRbL.transform.localPosition.x, 0, handRbL.transform.localPosition.z) : Vector3.zero;
            _handStartingPosR = handRbR ? new Vector3(handRbR.transform.localPosition.x, 0, handRbR.transform.localPosition.z) : Vector3.zero;
            _handTargetVecL = _handMinVec;
            _handTargetVecR = _handMinVec;
        }

        if (transform.parent == null)
        {
            Debug.LogWarning("Player " + transform.name + " is in improper player setup hierarchy");
        }
        else
        {
            // store local positions of all transforms in the player setup, and their children (if any) for restarting from checkpoint
            _startingTransformPositions = new List<Vector3>();
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                _startingTransformPositions.Add(transform.parent.GetChild(i).transform.localPosition);
            }
        }

        if (Camera.main is { }) _camTransform = Camera.main.transform;

        togAcc = gameObject.GetComponent<ToggleAccessories>();
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
        // change animation blend based on user input
        _animator.SetFloat("inputX", _moveInput.x);
        _animator.SetFloat("inputY", _moveInput.z);
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
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform c = transform.parent.GetChild(i);
            c.transform.localPosition = _startingTransformPositions[i];
            Rigidbody childRb = c.gameObject.GetComponent<Rigidbody>();
            if (childRb)
                childRb.velocity = Vector3.zero;
        }

        transform.parent.position = CheckpointController.lastCheckpoint.position;
    }
    
    public void onButtonA(InputAction.CallbackContext input)
    {
        togAcc.ToggleHat();
    }

    public void onButtonB(InputAction.CallbackContext input)
    {
        togAcc.ToggleBelt();
    }
    
}