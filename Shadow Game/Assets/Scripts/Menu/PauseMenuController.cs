using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject keyboardMenu;
    [SerializeField] private GameObject controllerMenu;

    [SerializeField] private GameObject pauseFirstButton;
    [SerializeField] private GameObject settingsFirstButton;
    [SerializeField] private GameObject controlsFirstButton;

    private bool switchMenu = false;

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
                backgroundImage.SetActive(true);

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void Controls()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
    }

    public void Switch()
    {
        switchMenu = !switchMenu;

        if (!switchMenu)
        {
            keyboardMenu.SetActive(true);
            controllerMenu.SetActive(false);
        }
        else if (switchMenu)
        {
            keyboardMenu.SetActive(false);
            controllerMenu.SetActive(true);
        }
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void Exit()
    {
        pauseMenu.SetActive(false);
        backgroundImage.SetActive(false);
    }
}
