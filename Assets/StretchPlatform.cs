using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[RequireComponent(typeof(Rigidbody))]
public class StretchPlatform : MonoBehaviour
{
    public float platformOffset = 0.5f;

    [Header("Hand Settings")] public float handStrength = 0.1f;
    public float handLocalMin = 0;
    public float handLocalMax = 2.0f;

    [Header("Transform References")] public Transform player1;
    public Transform player2;
    public Transform platformTarget;
    public Transform[] corners = new Transform[4];

    // starting positions / values
    private Vector3[] _cornerOffset = new Vector3[4];
    private float _xStartWidth, _zStartWidth, _startVolume, _handStartY = 0;
    private Vector3 _startScale;

    // target lerp t between handLocalMin and Max for each corner
    private float[] handTargetPos = new float[StaticClass.MAXPlayers * 2];

    private Rigidbody _rb;

    private void Start()
    {
        if (corners.Length != 4)
        {
            Debug.LogError("Invalid number of corners");
            return;
        }

        _rb = GetComponent<Rigidbody>();

        _xStartWidth = Mathf.Abs(corners[0].position.x - corners[3].position.x);
        _zStartWidth = Mathf.Abs(corners[0].position.z - corners[3].position.z);
        _startScale = transform.localScale;
        _startVolume = _startScale.x * _startScale.y * _startScale.z;
        if (_startVolume == 0)
            Debug.LogError(transform.name + " has an initial volume of 0");

        _handStartY = 0;
        for (int i = 0; i < 4; i++)
        {
            _cornerOffset[i] = transform.InverseTransformPoint(corners[i].position);
            _handStartY += corners[i].localPosition.y;
        }

        _handStartY /= 4; // corners should always start at same y position, but just in case
    }

    private void FixedUpdate()
    {
        float targetXWidth = Vector3.Distance(platformTarget.position, player1.position) + Vector3.Distance(platformTarget.position, player2.position) - platformOffset;
        float targetXScale = targetXWidth / _xStartWidth;

        float remainingVolume = _startVolume / targetXScale;

        Vector3 center = Vector3.Lerp(player1.position, player2.position, 0.5f);

        transform.localScale = new Vector3(targetXScale, remainingVolume / _startScale.z, _startScale.z);
        // _rb.MovePosition(center);
        platformTarget.GetComponent<Rigidbody>().MovePosition(center);


        //     _rb.AddForceAtPosition(Vector3.up * cornerForce.Item2 * handStrength, transform.position + cornerForce.Item1, ForceMode.Impulse);
        for (int i = 0; i < corners.Length; i++)
        {
            Vector3 cornerWorldPos = transform.TransformPoint(_cornerOffset[i]); 
            corners[i].position = cornerWorldPos;
            corners[i].localPosition += Vector3.up * Mathf.Lerp(handLocalMin, handLocalMax, handTargetPos[i]);

            Vector3 forceDirection = corners[i].position - cornerWorldPos;
            _rb.AddForceAtPosition(forceDirection * handStrength, cornerWorldPos);

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

        handTargetPos[player * StaticClass.MAXPlayers + hand] = t;

        // print("player=" + player + " hand=" + hand + "\noriginal offset="+corners[player][hand] + " new offset=" + localOffset);
    }
}