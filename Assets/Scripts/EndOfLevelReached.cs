using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndOfLevelReached : MonoBehaviour
{
    public GameObject youWon;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        print("trigger enter");
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                youWon = GameObject.Find("You Won");
                youWon.GetComponent<TMPro.TextMeshProUGUI>().text = "You Won";
            }

        }
    }
}
