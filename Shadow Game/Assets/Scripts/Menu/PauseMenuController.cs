using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject backgroundImage;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
                backgroundImage.SetActive(true);
            }
            else if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                pauseMenu.SetActive(false);
                settingsMenu.SetActive(false);
                controlsMenu.SetActive(false);
                backgroundImage.SetActive(false);
            }
        }
    }

    

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        backgroundImage.SetActive(false);
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Controls()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void Exit()
    {
        pauseMenu.SetActive(false);
        backgroundImage.SetActive(false);
    }
}
