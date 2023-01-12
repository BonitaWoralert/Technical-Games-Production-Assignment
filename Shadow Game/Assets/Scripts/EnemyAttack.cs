using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D attackBoxCollider;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private bool isPlayerTakingEnemyKnockback;
    public int enemyDamage;
    private PlayerStats playerStatsScript;
    private GameObject playerObject;
    private Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
        playerStatsScript = playerObject.GetComponent<PlayerStats>();
        isPlayerTakingEnemyKnockback = false;
    }

    private void Update()
    {
        if(isPlayerTakingEnemyKnockback == true)
        {
            Vector2 direction = (playerObject.transform.position - transform.position).normalized;
            playerRigidBody.AddForce(direction * knockbackStrength, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER TAKES DAMAGE");
            playerStatsScript.health -= enemyDamage;
            if(playerStatsScript.health <= 0)
            {
                //Player Death.
                Destroy(playerObject);
            }
            isPlayerTakingEnemyKnockback = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerTakingEnemyKnockback = false;
        }
    }
}
