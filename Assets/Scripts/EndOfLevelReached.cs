using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelReached : MonoBehaviour
{
    public GameObject youWon;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            youWon = GameObject.Find("You Won");
            youWon.GetComponent<TMPro.TextMeshProUGUI>().text = "You Won";
            
        }
    }
}
