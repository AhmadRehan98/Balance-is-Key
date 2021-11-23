using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndOfLevelReached : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("end of level trigger enter");
        if (other.CompareTag("Ball") || other.CompareTag("Player") && GameObject.FindWithTag("Ball") == null)
        {
            if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount) // go to next scene if we're on the main menu scene or tutorial level
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else // otherwise just reset the level and regenerate obstacles 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                LevelLayoutGenerator.level += 1;
            }
        }
    }
}
