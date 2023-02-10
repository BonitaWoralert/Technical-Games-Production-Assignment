using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private LoadingScene loadingScene;

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            buttonSound.Play();
            loadingScene.LoadScene(0);
        }
    }
}
