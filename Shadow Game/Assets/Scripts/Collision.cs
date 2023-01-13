using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    CameraController cam;
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
            cam = GameObject.FindObjectOfType<CameraController>();
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
