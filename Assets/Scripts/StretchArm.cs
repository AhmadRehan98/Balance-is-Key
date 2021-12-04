using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchArm : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tassel;
    public GameObject Hand;
    private Vector3 scale;
    private float step = 0.1f, last_dist, min_delta = 0.3f, max_delta = 1.0f;
    public float delta = 0.45f;
    private bool stretch;
    
    void Start()
    {
        stretch = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        last_dist = Vector3.Distance(Hand.transform.position, Tassel.transform.position);
        if (Vector3.Distance(Hand.transform.position, Tassel.transform.position) > delta)
        {
            scale = gameObject.transform.localScale;
            //Debug.Log("here");
            if (stretch)
            {
                gameObject.transform.localScale = new Vector3(scale.x, scale.y + step, scale.z);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(scale.x, scale.y - step, scale.z);
            }
            if (last_dist < Vector3.Distance(Hand.transform.position, Tassel.transform.position))
            {
                // Debug.Log("stretch flipped");
                stretch = !stretch;
                // step -= steps_step;
                delta = Math.Min(max_delta, delta + 0.02f);
            }
            else
            {
                // step += steps_step;
                delta = Math.Max(min_delta, delta - 0.01f);
            }
        }
        // Debug.Log("hand" + Hand.transform.position);
        // Debug.Log("tassel" + Tassel.transform.position);
        // Debug.Log(Vector3.Distance(Hand.transform.position, Tassel.transform.position));
        //Debug.Log(Vector3.Distance(Hand.transform.position, Tassel.transform.position));
        // Debug.Log(delta);
        
    }
}
