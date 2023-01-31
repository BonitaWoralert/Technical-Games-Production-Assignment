using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private float transitionTime = 0.5f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bool left = player.GetComponent<SpriteRenderer>().flipX;

        if (other.gameObject.tag == "Player" && gameObject.name == "Next Level Trigger")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            PlayerPrefs.SetInt("Spawn", 2);
            if (left)
                PlayerPrefs.SetInt("SpawnFlip", 1);
            else
                PlayerPrefs.SetInt("SpawnFlip", 2);
            PlayerPrefs.Save();
        }

        else if (other.gameObject.tag == "Player" && gameObject.name == "Previous Level Trigger")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
            PlayerPrefs.SetInt("Spawn", 1);
            if (left)
                PlayerPrefs.SetInt("SpawnFlip", 1);
            else
                PlayerPrefs.SetInt("SpawnFlip", 2);
            PlayerPrefs.Save();
        }

        else if (other.gameObject.tag == "Player" && gameObject.name == "Reload Level Trigger")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
            PlayerPrefs.SetInt("Spawn", 3);
            if (left)
                PlayerPrefs.SetInt("SpawnFlip", 1);
            else
                PlayerPrefs.SetInt("SpawnFlip", 2);
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
