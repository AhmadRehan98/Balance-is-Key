using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStretchingSounds : MonoBehaviour
{
    public GameObject startPoint, endPoint;
    
    private SoundManager sm;
    private float lastDistance;
    private float how_much_stretched;
    private float stretch_amount_to_play_sfx;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        how_much_stretched = 0.0f;
        stretch_amount_to_play_sfx = 2.0f;
        lastDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(startPoint.transform.position, endPoint.transform.position) >= lastDistance)
        {
            how_much_stretched = how_much_stretched +
                                 Vector3.Distance(startPoint.transform.position, endPoint.transform.position) -
                                 lastDistance;
        }
        else
        {
            how_much_stretched = 0.0f;
        }

        if (how_much_stretched >= stretch_amount_to_play_sfx)
        {
            sm.PlayArmStretch(0.7f);
        }

        lastDistance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
    }
}
