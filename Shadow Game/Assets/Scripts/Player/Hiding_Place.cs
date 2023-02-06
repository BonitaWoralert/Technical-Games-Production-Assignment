using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding_Place : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool isPlayerHiding;
    private Color defaultColor;
    private Color changedColor;
    //private SpriteRenderer spriteRenderer;

    private GameObject playerObject;
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerBoxCollider;

    private Animator animator;

    private bool hideFrame;

    void Start()
    {
        isPlayerInRange = false;
        isPlayerHiding = false;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //defaultColor = spriteRenderer.color;
        changedColor = Color.blue;

        playerObject = GameObject.Find("Player");
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
        playerBoxCollider = playerObject.GetComponent <BoxCollider2D>();
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hideFrame)
        {
            PlayerHiding();
        }
        PlayerHideInput();
    }

    private void PlayerHideInput()
    {
        if(isPlayerInRange == true || isPlayerHiding == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (isPlayerHiding == true)
                {
                    isPlayerHiding = false;
                    hideFrame = true;
                }
                else
                {
                    isPlayerHiding = true;
                    hideFrame = true;
                }
            }
        }
    }

    private void PlayerHiding()
    {
        //Player Box Collider is Disabled so that the enemy cannot detect the player.
        //Player Rigid Body Simulated is Disabled so that the player will not fall off the map (player can fall off the map because player box collider is disabled.))
        hideFrame = false;
        if(isPlayerHiding == true)
        {
            playerObject.transform.position = transform.position;
            playerRigidBody.velocity = Vector3.zero;
            //spriteRenderer.color = changedColor;
            playerSpriteRenderer.enabled = false;
            playerBoxCollider.enabled = false;
            playerRigidBody.simulated = false;
            animator.SetBool("isOccupied", true);
        }
        else
        {
            //spriteRenderer.color = defaultColor;
            playerSpriteRenderer.enabled = true;
            playerBoxCollider.enabled = true;
            playerRigidBody.simulated = true;
            playerObject.GetComponent<Movement>().CheckGrounded();
            animator.SetBool("isOccupied", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

}
