using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StretchPlatform : MonoBehaviour
{
    public float handStrength = 1.0f;
    
    public Transform player1, player2;
    public Transform platformTarget;
    public Transform cornerNE, cornerSW; // opposite corners, just to get starting dimensions

    private float _xStartWidth, _zStartWidth, _startVolume;
    private Vector3 _startScale;

    private Vector3[][] corners = new Vector3[StaticClass.MAXPlayers][];
    private Tuple<Vector3, float>[] currentCornerForces = new Tuple<Vector3, float>[StaticClass.MAXPlayers * 2];

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _xStartWidth = Mathf.Abs(cornerNE.position.x - cornerSW.position.x);
        _zStartWidth = Mathf.Abs(cornerNE.position.z - cornerSW.position.z);
        _startScale = transform.localScale;
        _startVolume = _startScale.x * _startScale.y * _startScale.z;
        if (_startVolume == 0)
            Debug.LogError(transform.name + " has an initial volume of 0");

        corners[0] = new Vector3[2];
        corners[0][0] = new Vector3(cornerNE.localPosition.x, cornerNE.localPosition.y, cornerSW.localPosition.z);
        corners[0][1] = cornerNE.localPosition;

        corners[1] = new Vector3[2];
        corners[1][0] = cornerSW.localPosition;
        corners[1][1] = new Vector3(cornerSW.localPosition.x, cornerSW.localPosition.y, cornerNE.localPosition.z);
    }

    private void FixedUpdate()
    {
        float targetXWidth = Vector3.Distance(platformTarget.position, player1.position) + Vector3.Distance(platformTarget.localPosition, player2.localPosition);
        float targetXScale = targetXWidth / _xStartWidth;

        float remainingVolume = _startVolume / targetXScale;

        Vector3 center = Vector3.Lerp(player1.position, player2.position, 0.5f);

        transform.localScale = new Vector3(targetXScale, remainingVolume / _startScale.z, _startScale.z);
        _rb.MovePosition(center);

        foreach (Tuple<Vector3,float> cornerForce in currentCornerForces)
        {
            if (cornerForce == null)
                continue;
            
            _rb.AddForceAtPosition(Vector3.up * cornerForce.Item2 * handStrength, cornerForce.Item1, ForceMode.Impulse);
            print(cornerForce);
        }
        
    }

    public void RaiseCorner(int player, int hand, float t)
    {
        if (player < 0 || player >= StaticClass.MAXPlayers)
        {
            Debug.LogError("Invalid player id \"" + player + "\"");
            return;
        }
        if (hand < 0 || hand >= 2)
        {
            Debug.LogError("Invalid hand \"" + hand + "\"");
            return;
        }

        Vector3 localOffset = corners[player][hand];
        localOffset.Scale(transform.localScale);

        currentCornerForces[player * StaticClass.MAXPlayers + hand] = new Tuple<Vector3, float>(localOffset, t);
        
        // print("player=" + player + " hand=" + hand + "\noriginal offset="+corners[player][hand] + " new offset=" + localOffset);
    }
}