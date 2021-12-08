using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinspeed = 40.0f;
    
    public GameObject[] fan_array;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject fan in fan_array)
        {
            fan.transform.Rotate(0, -1*spinspeed * Time.deltaTime, 0);
        }
    }
}
