using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBarFill;

    public void LoadScene(int sceneId)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressValue = operation.progress;
            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
