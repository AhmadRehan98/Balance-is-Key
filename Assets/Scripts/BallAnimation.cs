using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 0f;   
    }
    
    public void StopAnimation()
    {
        _animator.StopPlayback();
        gameObject.SetActive(false);
    }
}
