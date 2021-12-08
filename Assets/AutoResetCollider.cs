using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoResetCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>(); 
        if (p != null)
        {
            p.ResetPlayerSetup();
        }
    }
}
