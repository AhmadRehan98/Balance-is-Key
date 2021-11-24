using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndOfLevelReached : MonoBehaviour
{
    private GameObject winText;
    private void Start()
    {
       winText = GameObject.Find("You Won");
       winText?.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("end of level trigger enter");
        if (other.CompareTag("Ball") || other.CompareTag("Player") && GameObject.FindWithTag("Ball") == null)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) // go to next scene if we're on the main menu scene or tutorial level
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else // otherwise show the you won text
            {
                
                print(winText);
                if (winText != null)
                {
                    winText.SetActive(true);
                }
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                // LevelLayoutGenerator.level += 1;
            }
        }
    }
}
