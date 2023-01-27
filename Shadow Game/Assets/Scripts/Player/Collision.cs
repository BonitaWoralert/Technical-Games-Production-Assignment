using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    private CinemachineBrain cam;
    bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        SceneReset();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam = GameObject.FindObjectOfType<CinemachineBrain>();
            cam.enabled = false;
            isDead = true;
        }
    }

    public void SceneReset()
    {
        if (Input.GetKeyDown(KeyCode.R) && isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
