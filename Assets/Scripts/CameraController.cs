using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform[] followObjects;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 avg = Vector3.zero;
        foreach (Transform t in followObjects)
        {
            avg += t.position;
        }

        avg /= followObjects.Length;
        transform.position = avg - offset;
        print(transform.position);
    }
}
