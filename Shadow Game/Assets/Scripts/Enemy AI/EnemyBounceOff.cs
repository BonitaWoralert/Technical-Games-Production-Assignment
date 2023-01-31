using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounceOff : MonoBehaviour
{
    [SerializeField] private BoxCollider2D topBoxCollider;
    [SerializeField] private BoxCollider2D bottomBoxCollider;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float forcePush;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(isPlayerColliding)
        //{
        //    PlayerBounce();
        //}
    }

    private void PlayerBounce()
    {
        //playerRigidBody.AddForce((playerObject.transform.position - transform.position) * forcePush, ForceMode2D.Force);
        //playerRigidBody.AddForce((Vector2.right) * forcePush, ForceMode2D.Force);
        //playerRigidBody.AddForce((Vector2.right) * forcePush, ForceMode2D.Force);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerBounce();
            Debug.Log("Player Collided!!");
        }
    }

}
