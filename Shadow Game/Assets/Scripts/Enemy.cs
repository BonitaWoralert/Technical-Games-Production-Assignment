using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private AudioSource deathSound;
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
            SetDie();
        }
    }

    void SetDie()
    {
        Debug.Log("Enemy died!");
        deathSound.Play();
        //Play animation
        animator.SetBool("isDead", true);

        //Disable enemy
    }
    void Death()
    {
        Destroy(gameObject);
    }
}
