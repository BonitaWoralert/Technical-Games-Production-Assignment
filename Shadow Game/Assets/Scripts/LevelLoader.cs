using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    Animator transition;
    private float transitionTime = 1.0f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.name == "Next Level Trigger")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            PlayerPrefs.SetInt("Spawn", 2);
            PlayerPrefs.Save();
        }

        if (other.gameObject.tag == "Player" && gameObject.name == "Previous Level Trigger")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
            PlayerPrefs.SetInt("Spawn", 1);
            PlayerPrefs.Save();
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
