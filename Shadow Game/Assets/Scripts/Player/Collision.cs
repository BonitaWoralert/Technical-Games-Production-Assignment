using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    Animator transition;
    private bool isDead = false;
    private float transitionTime = 1.0f;
    private CinemachineBrain cam;
    GameObject box;
    // Update is called once per frame
    void Update()
    {
        StartCoroutine ( SceneReset() );
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            cam = GameObject.FindObjectOfType<CinemachineBrain>();
            cam.enabled = false;
            isDead = true;
        }
        if (other.gameObject.name == "InvincibleCollisionBox")
        {
            cam = GameObject.FindObjectOfType<CinemachineBrain>();
            cam.enabled = false;
            isDead = true;
        }
    }

    IEnumerator SceneReset()
    {
        if (isDead)
        {
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}