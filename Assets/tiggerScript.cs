using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Material Default_Material, Triggered_Material;
    public float spinspeed_nonZero = 20.0f;
    private float spinspeed;
    public GameObject bridge;
    void Start()
    {
        spinspeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bridge.transform.Rotate(0, -1*spinspeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer render = GetComponent<Renderer>();
            render.material = Triggered_Material;
            spinspeed = spinspeed_nonZero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        render.material = Default_Material;
        spinspeed = 0;
    }
}
