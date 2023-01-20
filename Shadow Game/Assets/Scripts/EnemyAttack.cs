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
    [SerializeField] private int enemyHealth;
    private PlayerStats playerStatsScript;
    private GameObject playerObject;
    //private SpriteRenderer playerSpriteRenderer;
    //private Color defaultPlayerColor;
    private Rigidbody2D playerRigidBody;
    [SerializeField] Enemy enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
        playerStatsScript = playerObject.GetComponent<PlayerStats>();
        isPlayerTakingEnemyKnockback = false;
        enemyScript = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        if(isPlayerTakingEnemyKnockback == true)
        {
            Vector2 direction = (playerObject.transform.position - transform.position).normalized;
            //playerRigidBody.AddForce(new Vector2(direction.x * knockbackStrength, 0), ForceMode2D.Force);
            //playerRigidBody.AddForce(direction * knockbackStrength, ForceMode2D.Force);
            playerRigidBody.velocity = knockbackStrength * direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" )
        {
            if (enemyScript.currentHealth > 0)
            {
                Debug.Log("PLAYER TAKES DAMAGE");
                playerStatsScript.health -= enemyDamage;
            }
            Vector2 direction = (playerObject.transform.position - transform.position).normalized;
            //playerRigidBody.AddForce(new Vector2(direction.x * knockbackStrength, 0), ForceMode2D.Impulse);
            playerRigidBody.velocity = direction * knockbackStrength;
            //playerSpriteRenderer.color = Color.white;
            isPlayerTakingEnemyKnockback = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //playerSpriteRenderer.color = defaultPlayerColor;
            isPlayerTakingEnemyKnockback = false;
        }
    }
}