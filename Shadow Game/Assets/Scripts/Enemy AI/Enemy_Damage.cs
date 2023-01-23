using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRigidBody;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Player_Invincibility playerInvincibilityScript;
    private BoxCollider2D slimeAttackBox;

    private void Start()
    {
        enemyScript = GetComponentInParent<Enemy>();
        slimeAttackBox = GetComponentInParent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (playerRigidBody.velocity.y < -0.05f)
            {
                Debug.Log("PLAYER DAMAGES ENEMY");
                enemyScript.TakeDamage(100);
                if(enemyScript.currentHealth <= 0)
                {
                    slimeAttackBox.enabled = false;
                }
            }
        }
    }
}
