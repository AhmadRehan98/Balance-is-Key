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
    // public Rigidbody handRbL, handRbR;
    public StretchPlatform platform;
    
    private Rigidbody _bodyRb;
    private Transform _camTransform; // camera transform for relative inputs
    private Animator _animator;


    // body parameters
    public float bodyAcceleration = 50f; // net force increase per FixedUpdate call
    
    // user input vector
    private Vector3 _moveInput = Vector3.zero;

    // starting local positions for checkpoint system
    private List<Vector3> _startingTransformPositions;
    private List<Vector3> _startingTransformRotations;
    private List<Vector3> _startingTransformScales;
    
    // for toggling accessories/cosmetics
    Accessories _accessories;


    void Start()
    {
        // assign component references
        _bodyRb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            Debug.LogWarning(transform.name + "Missing AnimatorController");

        if (transform.parent == null)
        {
            Debug.LogWarning("Player " + transform.name + " is in improper player setup hierarchy");
        }
        else
        {
            // store local positions of all transforms in the player setup, and their children (if any) for restarting from checkpoint
            _startingTransformPositions = new List<Vector3>();
            _startingTransformRotations = new List<Vector3>();
            _startingTransformScales = new List<Vector3>();
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                _startingTransformPositions.Add(transform.parent.GetChild(i).transform.localPosition);
                _startingTransformRotations.Add(transform.parent.GetChild(i).transform.localEulerAngles);
                _startingTransformScales.Add(transform.parent.GetChild(i).transform.localScale);
            }
        }

        if (Camera.main is { }) _camTransform = Camera.main.transform;

        _accessories = gameObject.GetComponent<Accessories>();
    }


    void FixedUpdate()
    {
        // for player movement, adjust the net force vector every frame by adding a force in the direction of the user input
        _bodyRb.AddForce(_moveInput, ForceMode.Force);
        // change animation blend based on user input
        _animator.SetFloat("inputY", _moveInput.x);
        _animator.SetFloat("inputX", _moveInput.z);
    }

    public void OnPlayerMove(InputAction.CallbackContext input)
    {
        Vector2 inVec = input.ReadValue<Vector2>();
        float mag = Mathf.Lerp(0, 1,inVec.magnitude);
        // print($"{inVec} {inVec.magnitude}");
        if (_camTransform != null)
        {
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
            _moveInput.x = inVec.x;
            _moveInput.y = 0;
            _moveInput.z = inVec.y;
        }
        _moveInput.Normalize();
        _moveInput *= Mathf.Lerp(0f, bodyAcceleration, mag);
    }

    public void OnLeftArm(InputAction.CallbackContext input)
    {
        platform.RaiseCorner(_accessories.player, 0, input.ReadValue<float>());
    }

    public void OnRightArm(InputAction.CallbackContext input)
    {
        platform.RaiseCorner(_accessories.player, 1, input.ReadValue<float>());
    }

    public void OnResetScene(InputAction.CallbackContext input)
    {
        if (input.started == false) // when button is released dont do anything
        {
            return;
        }
        ResetPlayerSetup();
    }

    public void ResetPlayerSetup()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            Transform c = transform.parent.GetChild(i);
            c.transform.localPosition = _startingTransformPositions[i];
            c.transform.localEulerAngles = _startingTransformRotations[i];
            c.transform.localScale = _startingTransformScales[i];
            Rigidbody childRb = c.gameObject.GetComponent<Rigidbody>();
            if (childRb)
            {
                childRb.velocity = Vector3.zero;
                childRb.angularVelocity = Vector3.zero;
            }
        }

        transform.parent.position = CheckpointController.lastCheckpoint.position;
        OnScreenStats.NumResets++;
        OnScreenStats.PauseTimer = false;
    }

    public void OnPause(InputAction.CallbackContext input)
    {
        if (input.started == false) // when button is released dont do anything
        {
            return;
        }
        PauseMenu.PauseButtonPressed = true;
    }
    
    public void onButtonA(InputAction.CallbackContext input)
    {
        if (input.started == false) // when button is released dont do anything
        {
            return;
        }
        if(SceneManager.GetActiveScene().name == "CharacterSelect") 
            _accessories.ToggleHat();  
    }

    public void onButtonB(InputAction.CallbackContext input)
    {
        if (input.started == false) // when button is released dont do anything
        {
            return;
        }
        if(SceneManager.GetActiveScene().name == "CharacterSelect") 
            _accessories.ToggleBelt();
    }
    
    public void onButtonY(InputAction.CallbackContext input)
    {
        if (input.started == false) // when button is released dont do anything
        {
            return;
        }
        if(SceneManager.GetActiveScene().name == "CharacterSelect") 
            _accessories.NextSkin();
    }
    
}