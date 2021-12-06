using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchPlatform : MonoBehaviour
{
    public Transform player1, player2;
    public Transform platformTarget;
    public Transform cornerNE, cornerSW; // opposite corners, just to get starting dimensions
    
    private float _xStartWidth, _zStartWidth;

    private Rigidbody _rb;
    
    private void Start()
    {
        _xStartWidth = Mathf.Abs(cornerNE.position.x - cornerSW.position.x);
        _zStartWidth = Mathf.Abs(cornerNE.position.z - cornerSW.position.z);
        _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        float targetWidth = Vector3.Distance(platformTarget.position, player1.position) + Vector3.Distance(platformTarget.localPosition,player2.localPosition) ;
        float targetScale = targetWidth / _xStartWidth;

        Vector3 center = Vector3.Lerp(player1.position, player2.position, 0.5f);
        
        transform.localScale = new Vector3(targetScale, 1, 1);
        _rb.MovePosition(center);
        
    }

    public void RaiseCorner(int player, int hand, float t)
    {
        
    }
    
}