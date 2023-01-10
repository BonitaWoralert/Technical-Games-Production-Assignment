using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Aarons Scene");
    }
    
    public void Controls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
