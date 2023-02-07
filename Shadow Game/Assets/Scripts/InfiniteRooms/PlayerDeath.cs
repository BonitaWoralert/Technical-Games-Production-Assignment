using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    GameObject player;
    private Animator ani;
    bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerStats>().health <= 0 && !isDead)
        {
            Debug.Log("You have died like an idiot");
            isDead = true;
            Death();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Death();
        }
    }

    private void Death()
    {
        player.GetComponent<Movement>().enabled = false;
        player.GetComponent<ShadowMovement>().enabled = false;
        player.GetComponent<Combat>().enabled = false;
        player.GetComponent<ShadowForm>().enabled = false;
        ani.SetInteger("state", 0);
        ani.SetTrigger("deathTrigger");
        Invoke("ChangeScene", 1.5f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("DeathMenu");
    }
}
