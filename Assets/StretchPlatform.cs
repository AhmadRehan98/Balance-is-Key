using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchPlatform : MonoBehaviour
{
    public Transform ne, nw, se, sw;
    public Rigidbody platformTarget;

    private Rect _targetShape;
    private Rect _startShape;

    private float verticalWidth()
    {
        Vector3 northEdge = Vector3.Lerp(ne.localPosition, nw.localPosition, 0.5f);
        Vector3 southEdge = Vector3.Lerp(se.localPosition, sw.localPosition, 0.5f);

        return (northEdge - southEdge).magnitude;
    }

    private float horizontalWidth()
    {
        Vector3 westEdge = Vector3.Lerp(nw.localPosition, sw.localPosition, 0.5f);
        Vector3 eastEdge = Vector3.Lerp(ne.localPosition, se.localPosition, 0.5f);

        return (westEdge - eastEdge).magnitude;
    }

    private void recalculateRect()
    {
        Vector3 avgPos = (ne.localPosition + nw.localPosition + se.localPosition + sw.localPosition) / 4.0f;
        _targetShape.x = avgPos.x;
        _targetShape.y = avgPos.z;
        _targetShape.width = horizontalWidth();
        _targetShape.height = verticalWidth();
    }


    private void Start()
    {
        _startShape = new Rect(0, 0, horizontalWidth(), verticalWidth());
        _targetShape = new Rect(_startShape);
    }

    private void FixedUpdate()
    {
        recalculateRect();
        platformTarget.MovePosition(new Vector3(_targetShape.x, platformTarget.position.y, _targetShape.y));
        transform.localScale = new Vector3( _targetShape.width / _startShape.width, 1, 1);
        // transform.localRotation = Quaternion.Euler(0, Vector3.Angle(ne.localPosition - se.localPosition, ne.localPosition - nw.localPosition), 0);
    }
}