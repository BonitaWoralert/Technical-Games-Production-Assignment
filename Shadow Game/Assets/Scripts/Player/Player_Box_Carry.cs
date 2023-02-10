using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Box_Carry : MonoBehaviour
{
    [SerializeField] private BoxCollider2D grabColliderBox;
    private BoxCollider2D puzzleBoxBoxCollider;
    [SerializeField] private bool isBoxInRange;
    [SerializeField] private bool isCarryingBox;
    [SerializeField] private float carryMass;
    [SerializeField] private float throwStrength;
    [SerializeField] private float boxYOffset;
    [SerializeField] private float throwAngleX;
    [SerializeField] private float throwAngleY;
    private GameObject boxToGrab;
    private Rigidbody2D boxRigidBody; 
    private GameObject playerGameObject;
    //private GameObject grabCheckObject;
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        //isBoxInRange = false;
        //isCarryingBox = false;
        playerGameObject = GameObject.Find("Player");
        playerRigidbody = playerGameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void Update()
    { 
        if(Input.GetButtonDown("Interact"))
        {
            if(isCarryingBox == true)
            {
                BoxDrop();
            }
            else
            {
                BoxCarry();
            }

        }

        if(spriteRenderer.flipX == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (isCarryingBox == true)
        {
            playerRigidbody.mass = carryMass;
        }
        else
        {
            playerRigidbody.mass = 1;
        }
    }

    private void BoxCarry()
    {
        if((boxToGrab != null && isBoxInRange == true) && boxRigidBody != null)
        {
            boxToGrab.transform.SetParent(playerGameObject.transform, true);
            boxToGrab.transform.localPosition = new Vector3(0, 0.2f, 0);
            boxToGrab.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            boxRigidBody.simulated = false;
            isCarryingBox = true;
            grabColliderBox.enabled = false;
            //puzzleBoxBoxCollider.enabled = false;
        }
    }

    private void BoxDrop()
    {
        boxRigidBody.simulated = true;
        Vector2 direction;

        if (playerRigidbody.velocity.y > 0.05f || playerRigidbody.velocity.y < -0.05f)
        {
            boxToGrab.transform.localPosition = new Vector3(0, boxYOffset, 0);
            //Player is Jumping
            if (spriteRenderer.flipX == true)
            {
                direction = new Vector2(-throwAngleX, throwAngleY);
            }
            else
            {
                direction = new Vector2(throwAngleX, throwAngleY);
                //direction = new Vector2(0.7f, 0.4f);
            }
            //Vector2 direction = (boxToGrab.transform.position - transform.position).normalized;
            //Vector2 direction = (transform.position - boxToGrab.transform.position).normalized;
            boxRigidBody.velocity += new Vector2((throwStrength * direction.x), (throwStrength * direction.y));
            boxRigidBody.velocity += playerRigidbody.velocity;
            isBoxInRange = false;
        }
        else
        {
            //Player is onLand.
            if (spriteRenderer.flipX == true)
            {
                boxToGrab.transform.localPosition = new Vector3(-0.2f, 0, 0);
            }
            else
            {
                boxToGrab.transform.localPosition = new Vector3(0.2f, 0, 0);
            }
        }


        boxToGrab.transform.SetParent(null, true);
        grabColliderBox.enabled = true;
        //puzzleBoxBoxCollider.enabled = true;
        isCarryingBox = false;
    }

    public void GrabReset()
    {
        isCarryingBox = false;
        grabColliderBox.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PuzzleBox")
        {
            isBoxInRange = true;
            boxToGrab = collision.gameObject;
            boxRigidBody = boxToGrab.GetComponent<Rigidbody2D>();
            //boxDefaultRigidBodyDrag = boxRigidBody.drag;
            //boxDefaultRigidBodyAngularDrag = boxRigidBody.angularDrag;
            //boxDefaultRigidBodyGravityScale = boxRigidBody.gravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isCarryingBox == false && collision.gameObject.tag == "PuzzleBox")
        {
            isBoxInRange = false;
            boxToGrab = null;
            boxRigidBody = null;
        }
    }
}
