using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public static bool PauseButtonPressed;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        GameIsPaused = false;
        PauseButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     if (GameIsPaused)
        //     {
        //         Resume();
        //     }
        //     else
        //     {
        //         Pause();
        //     }
        // }

        if (PauseButtonPressed)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

            PauseButtonPressed = false;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void RestartLevel()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(idx);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    
}