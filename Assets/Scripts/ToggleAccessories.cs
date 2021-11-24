using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class ToggleAccessories : MonoBehaviour
{
    private GameObject child, hat, belt;

    public int player;
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
        Debug.Log(belt.name);
        Debug.Log(hat.name);
        if (player == 1)
            StaticClass.hat_visible_p1 = StaticClass.belt_visible_p1 = true;
        else if (player == 2)
            StaticClass.hat_visible_p2 = StaticClass.belt_visible_p2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHat()
    {
        if (player == 1) {
            StaticClass.hat_visible_p1 = !StaticClass.hat_visible_p1;
            hat.SetActive(StaticClass.hat_visible_p1);
        }
        else if (player == 2) {
            StaticClass.hat_visible_p2 = !StaticClass.hat_visible_p2;
            hat.SetActive(StaticClass.hat_visible_p2);
        }
    }

    public void RemoveBelt()
    {
        if (player == 1) {
            StaticClass.belt_visible_p1 = !StaticClass.belt_visible_p1;
            hat.SetActive(StaticClass.belt_visible_p1);
        }
        else if (player == 2) {
            StaticClass.belt_visible_p2 = !StaticClass.belt_visible_p2;
            hat.SetActive(StaticClass.belt_visible_p2);
        }
    }
}
