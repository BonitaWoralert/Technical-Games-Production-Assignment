using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    private CameraController bob;
    private bool noCollision = true;

    // Update is called once per frame
    void Update()
    {
        if (noCollision == false)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Debug.Log("Hi");
                SceneManager.LoadScene("Tutorial");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Dead");
            bob = GameObject.FindObjectOfType<CameraController>();
            bob.enabled = false;
            noCollision = false;
        }
    }
}
