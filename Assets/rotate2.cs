using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinspeed = 40.0f;
    
    

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
            this.transform.Rotate(0, -1*spinspeed * Time.deltaTime, 0);
    }
}
