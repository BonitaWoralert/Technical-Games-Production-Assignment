using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    //private Enemy_AI_v2 enemyAIScript;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        //Play animation
        animator.SetTrigger("deadTrigger");

        //Disable enemy
        GetComponent<Collider2D>().enabled = true;
        this.enabled = false;
    }
}
