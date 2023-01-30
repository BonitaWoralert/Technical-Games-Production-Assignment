using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenCheck : MonoBehaviour
{
    private Vector2 resolution;

    private void Awake()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        if (resolution.x != Screen.width || resolution.y != Screen.height)
        {
            SceneManager.LoadScene(0);

            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }
    }
}
