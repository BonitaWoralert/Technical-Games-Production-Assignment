using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D playerRigidBody;
    [SerializeField] Enemy enemyScript;

    private void Start()
    {
        enemyScript = GetComponentInParent<Enemy>();
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
            }
        }
    }
}
