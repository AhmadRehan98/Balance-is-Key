using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchArm : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tassel;
    public GameObject Hand;
    public float delta = 0.45f;

    private SoundManager sm;
    private Vector3 scale;
    private float step = 0.1f, last_dist, min_delta = 0.3f, max_delta = 1.0f;
    private bool stretch;
    private float how_much_stretched;
    private float stretch_amount_to_play_sfx;
    
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        stretch = true;
        how_much_stretched = 0.0f;
        stretch_amount_to_play_sfx = 2.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(how_much_stretched);
        last_dist = Vector3.Distance(Hand.transform.position, Tassel.transform.position);
        if (Vector3.Distance(Hand.transform.position, Tassel.transform.position) > delta)
        {
            scale = gameObject.transform.localScale;
            
            //Debug.Log("here");
            if (stretch)
            {
                how_much_stretched += Vector3.Distance(Hand.transform.position, Tassel.transform.position);
                gameObject.transform.localScale = new Vector3(scale.x, scale.y + step, scale.z);
            }
            else
            {
                how_much_stretched = 0.0f;
                gameObject.transform.localScale = new Vector3(scale.x, scale.y - step, scale.z);
            }

            if (how_much_stretched >= stretch_amount_to_play_sfx)
            {
                //sm.PlayArmStretch();
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
