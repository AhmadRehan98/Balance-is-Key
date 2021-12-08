using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.speed = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("door trigger enter");
        if (other.CompareTag("Ball") || other.CompareTag("Player") && GameObject.FindWithTag("Ball") == null)
        {
            _animator.speed = 0.5f;
            Animator ball = GameObject.FindGameObjectsWithTag("Ball")[0].GetComponent<Animator>();
            ball.speed = 0.5f;
            
            //Debug.Log(ball.GetCurrentAnimatorClipInfo(0)[0].clip.legacy);
            //ball.StopPlayback();
        }
    }
}
