using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform[] followObjects;
    private Vector3 _offset;
    public float autoZoomStep=0.2f;
    private float maxViewportDistance = 0.85f;
    private Vector3 lookDirection;
    public int zoomLevelBoundaries_max, zoomLevelBoundaries_min;
    private float zoom_Velocity= 0;

    private Vector3 target_position;
    // Start is called before the first frame update
    void Start()
    {
        target_position = CalcAveragePos();
        _offset = target_position - transform.position;
    }

    // returns the average position of every element of followObjects
    Vector3 CalcAveragePos()
    {
        if (followObjects.Length == 0) return Vector3.zero;
        
        Vector3 avg = Vector3.zero;
        foreach (Transform t in followObjects)
        {
            avg += t.position;
        }

        avg /= followObjects.Length;
        return avg;
    }
    
    private float zoomLevel = 0.0f;
    // Transformed zoom level
    private float zoomValue = 1.0f;
    // Hides transformation of the zoom level into the zoom value
    // When the zoom level is set, the zoom value is automatically calculated
    public float ZoomLevel
    {
        get { return zoomLevel; }
        set
        {
            // Recalculate only if zoom level changed
            if (zoomLevel != value)
            {
                zoomLevel = value;
                // Consider zoom level boundaries and calculate zoom value
                zoomLevel = Mathf.Clamp(zoomLevel, zoomLevelBoundaries_min, zoomLevelBoundaries_max);
                zoomValue = Mathf.Pow(1.1f, -zoomLevel);
            }
        }
    }
    private Vector3 CalcCameraPos()
    {
                    Vector2 playerAViewport = Camera.main.WorldToViewportPoint(followObjects[0].position);
                    Vector2 playerBViewport = Camera.main.WorldToViewportPoint(followObjects[1].position);
                    float viewportDistance = Vector2.Distance(playerAViewport, playerBViewport);
                    // If the viewport distance between the player and the enemy is too big, zoom out a little bit
                    if (viewportDistance > maxViewportDistance)
                    {
                        ZoomLevel = Mathf.SmoothDamp(ZoomLevel, ZoomLevel - autoZoomStep, ref zoom_Velocity, Time.deltaTime);
                    }
                
            
            // Calculate the new camera position. Steps:
            //  - Position it at the target's position
            //  - Rotate it towards the target and consider camera tilting
            //  - Given this rotation move the camera backwards by the zoom value
            return target_position - Quaternion.LookRotation(new Vector3(lookDirection.x, lookDirection.y, lookDirection.z))
                *  Vector3.forward * 4.0f * zoomValue;
        }

    private void FixedUpdate()
    {
        Vector3 targetPosition = CalcAveragePos();
        transform.position = CalcCameraPos();
        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position); // this is not final
    }
}
