using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    private LayerMask enemyLayers;

    private int attackDamage = 50;
    private float attackRange = 0.5f;
    private float attackRate = 10.0f;
    private float nextAttackTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                BasicAttack();
                Debug.Log("Player is attacking!");
                nextAttackTime = Time.time + 1.0f / attackRate;
            }
        }
    }

    void BasicAttack()
    {
        //Plays the attack animation
        //animator.SetTrigger("Attack");
        animator.SetInteger("state", 6);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
}