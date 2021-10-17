using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class GenerateBoxColliderBowl : MonoBehaviour
{
    private const float PI = Mathf.PI;
    
    public float xRadius=1, yRadius=1, zRadius=1; // radius of sphere on each axis
    public float colliderThickness = 0.1f; // thickness of each BoxCollider
    public float thetaResolution = 10; // number of colliders on each horizontal layer of sphere
    public float phiResolution = 5; // number of layers for half of sphere

    [ContextMenu("Generate")]
    void Generate()
    {
        // remove all colliders from previous runs
        Clear();
        
        Vector3 center = new Vector3(0, yRadius, 0);

        float phiStep =  (PI * yRadius) / phiResolution; // only bottom half of sphere, double for full sphere

        float phi = -PI;
        float theta = 0;
        for (int i = 0; i < phiResolution; i++)
        {
            // i hate this lmao
            gameObject.AddComponent<BoxCollider>();
            BoxCollider[] boxes = gameObject.GetComponents<BoxCollider>();
            BoxCollider b = boxes.Last();
            b.center = new Vector3(
                xRadius * Mathf.Cos(theta),
                yRadius * Mathf.Cos(phi),
                zRadius * Mathf.Sin(theta)
                );
            b.size = new Vector3(0.3f, 0.3f, 0.3f);
            
            phi += phiStep;
        }


    }

    [ContextMenu("Clear Box Colliders")]
    void Clear()
    {
        foreach (BoxCollider c in gameObject.GetComponents<BoxCollider>())
        {
            DestroyImmediate(c);
        }
    }
}
