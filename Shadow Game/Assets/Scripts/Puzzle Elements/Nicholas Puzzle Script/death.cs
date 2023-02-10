using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    PlayerStats stats;
    GameObject player;
    private Animator ani;
    [SerializeField] private Vector2 playerStartingPos;
    private bool canDie;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        ani = player.GetComponent<Animator>();
        playerStartingPos = player.transform.position;
        canDie = true;
    }

    private void Update()
    {
        Respawn();
    }

    private void Respawn()
    {
        if (stats.health <= 0 && canDie)
        {
            canDie = false;
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<ShadowMovement>().enabled = false;
            player.GetComponent<Combat>().enabled = false;
            player.GetComponent<ShadowForm>().enabled = false;
            ani.SetInteger("state", 0);
            ani.SetTrigger("deathTrigger");
            StartCoroutine("ChangePos");
        }
    }

    IEnumerator ChangePos()
    {
        yield return new WaitForSeconds(1.5f);
        stats.health = stats.maxHealth;
        canDie = true;
        ani.SetInteger("state", 2);
        yield return new WaitForEndOfFrame();
        ani.SetInteger("state", 0);
        player.transform.position = playerStartingPos;
    }
}
