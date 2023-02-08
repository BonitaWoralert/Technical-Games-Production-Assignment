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
    [SerializeField] private GameObject controllerFirstButton;

    [SerializeField] private AudioSource buttonSound;

    private bool switchMenu = false;

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (Time.timeScale == 1.0f)
            {
                buttonSound.Play();

                Time.timeScale = 0.0f;
                pauseMenu.SetActive(true);
                backgroundImage.SetActive(true);

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
            else if (Time.timeScale == 0.0f)
            {
                buttonSound.Play();

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
        buttonSound.Play();

        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        backgroundImage.SetActive(false);
    }

    public void Settings()
    {
        buttonSound.Play();

        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void Controls()
    {
        buttonSound.Play();

        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
    }

    public void Switch()
    {
        buttonSound.Play();

        switchMenu = !switchMenu;

        if (!switchMenu)
        {
            keyboardMenu.SetActive(true);
            controllerMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(controlsFirstButton);
        }
        else if (switchMenu)
        {
            keyboardMenu.SetActive(false);
            controllerMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(controllerFirstButton);
        }
    }

    public void Back()
    {
        buttonSound.Play();

        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void Exit()
    {
        buttonSound.Play();

        pauseMenu.SetActive(false);
        backgroundImage.SetActive(false);
    }
}
