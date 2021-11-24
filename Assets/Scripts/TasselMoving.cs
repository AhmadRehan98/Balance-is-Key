using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TasselMoving : MonoBehaviour
{
    private Vector3 lastPos;
    public AudioSource audiosrc;
    // private SoundManager sm;
    private float positionSensetivity = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        audiosrc = gameObject.GetComponent<AudioSource>();
        // sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        lastPos = gameObject.GetComponent<Rigidbody>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float y_movement = Math.Abs(gameObject.GetComponent<Rigidbody>().transform.position.y - lastPos.y);
        // Debug.Log(y_movement);
        
        // audiosrc.pitch = Random.Range(0.95f, 1.05f);
        if (y_movement > positionSensetivity && !audiosrc.isPlaying)
        {
            audiosrc.volume = Random.Range(0.1f, 0.2f);
            audiosrc.Play();
            // sm.PlayHandMove();
        }
        
        
        lastPos = new Vector3(gameObject.GetComponent<Rigidbody>().transform.position.x,
            gameObject.GetComponent<Rigidbody>().transform.position.y,
            gameObject.GetComponent<Rigidbody>().transform.position.z);
    }
}
