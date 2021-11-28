using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))] 

public class IKControlHead : MonoBehaviour {
    
    protected Animator animator;
    
    public bool ikActive = false;
    // public Transform headObj = null;
    public Transform lookObj = null;

    void Start () 
    {
        animator = GetComponent<Animator>();
    }
    
    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {
            
            //if the IK is active, set the position and rotation directly to the goal. 
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }    

                // Set the right hand target position and rotation, if one has been assigned
                // if(headObj != null) {
                //     animator.SetLookAtPosition(lookObj.position);
                // }        
            }
            
            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                animator.SetLookAtWeight(0);
            }
        }
    }    
}