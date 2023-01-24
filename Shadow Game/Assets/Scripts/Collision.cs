using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    CameraController cam;
    Animator transition;
    private bool isDead = false;
    private float transitionTime = 1.0f;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine ( SceneReset() );
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam = GameObject.FindObjectOfType<CameraController>();
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