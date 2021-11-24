using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class ToggleAccessories : MonoBehaviour
{
    private GameObject child, hat, belt; 
    bool hat_visible, belt_visible;
    // Start is called before the first frame update
    void Start()
    {
        child = gameObject;
        for (int i = 0; i < 1; i++)
        {
            // Debug.Log(child.name);
            child = child.transform.GetChild(0).gameObject;
        }
        child = child.transform.GetChild(1).gameObject;
        child = child.transform.GetChild(1).gameObject;
        child = child.transform.GetChild(0).gameObject;
        belt = child.transform.GetChild(0).gameObject;
        hat = child.transform.GetChild(1).gameObject;
        // Debug.Log(belt.name);
        // Debug.Log(hat.name);
        hat_visible = belt_visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHat()
    {
        hat_visible = !hat_visible;
        hat.SetActive(hat_visible);
    }

    public void RemoveBelt()
    {
        belt_visible = !belt_visible;
        belt.SetActive(belt_visible);
    }
}
