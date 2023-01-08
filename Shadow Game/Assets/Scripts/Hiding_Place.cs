using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding_Place : MonoBehaviour
{
    bool isPlayerInRange;
    bool isPlayerHiding;
    Color defaultColor;
    Color changedColor;
    SpriteRenderer spriteRenderer;

    GameObject playerObject;
    SpriteRenderer playerSpriteRenderer;
    Rigidbody2D playerRigidBody;
    BoxCollider2D playerBoxCollider;

    void Start()
    {
        isPlayerInRange = false;
        isPlayerHiding = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        changedColor = Color.blue;

        playerObject = GameObject.Find("Player");
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
        playerBoxCollider = playerObject.GetComponent <BoxCollider2D>();
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerHideInput();
        PlayerHiding();
    }

    private void PlayerHideInput()
    {
        if(isPlayerInRange == true || isPlayerHiding == true)
        {
            if (Input.GetKeyDown("e"))
            {
                isPlayerHiding = !isPlayerHiding;
            }
        }
    }

    private void PlayerHiding()
    {
        //Player Box Collider is Disabled so that the enemy cannot detect the player.
        //Player Rigid Body Simulated is Disabled so that the player will not fall off the map (player can fall off the map because player box collider is disabled.))
        if(isPlayerHiding == true)
        {
            spriteRenderer.color = changedColor;
            playerSpriteRenderer.enabled = false;
            playerBoxCollider.enabled = false;
            playerRigidBody.simulated = false;
        }
        else
        {
            spriteRenderer.color = defaultColor;
            playerSpriteRenderer.enabled = true;
            playerBoxCollider.enabled = true;
            playerRigidBody.simulated = true;
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
