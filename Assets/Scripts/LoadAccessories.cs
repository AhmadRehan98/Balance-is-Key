using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAccessories : MonoBehaviour
{
    private GameObject child, hat, belt;

    [Range(1, 2)]
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

        if (player == 1) {
            belt.SetActive(StaticClass.belt_visible_p1);
            hat.SetActive(StaticClass.hat_visible_p1);
            Debug.Log("hat: " + StaticClass.hat_visible_p1);
            Debug.Log("belt: " + StaticClass.belt_visible_p1);
        }
        else if (player == 2) {
            belt.SetActive(StaticClass.belt_visible_p2);
            hat.SetActive(StaticClass.hat_visible_p2);
            Debug.Log("hat: " + StaticClass.hat_visible_p2);
            Debug.Log("belt: " + StaticClass.belt_visible_p2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
