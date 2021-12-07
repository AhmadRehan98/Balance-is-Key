using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_test : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    void Start()
    {
        this.transform.RotateAround(target.transform.position, Vector3.up, 90);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
