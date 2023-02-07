using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject keyboardMenu;
    [SerializeField] private GameObject controllerMenu;
    [SerializeField] private GameObject creditsMenu;

    [SerializeField] private GameObject mainFirstButton;
    [SerializeField] private GameObject levelsFirstButton;
    [SerializeField] private GameObject settingsFirstButton;
    [SerializeField] private GameObject controlsFirstButton;
    [SerializeField] private GameObject controls2FirstButton;
    [SerializeField] private GameObject creditsFirstButton;

    [SerializeField] private AudioSource buttonSelect;

    private bool switchMenu = false;

    public void Levels()
    {
        buttonSelect.Play();

        mainMenu.SetActive(false);
        levelsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelsFirstButton);
    }

    public void Settings()
    {
        buttonSelect.Play();

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void Controls()
    {
        buttonSelect.Play();

        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
    }

    public void Switch()
    {
        buttonSelect.Play();

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
            EventSystem.current.SetSelectedGameObject(controls2FirstButton);
        }
    }

    public void Credits()
    {
        buttonSelect.Play();

        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsFirstButton);
    }

    public void Back()
    {
        buttonSelect.Play();

        mainMenu.SetActive(true);
        levelsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainFirstButton);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
