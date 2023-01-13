using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxSpeed = 15f;
    public float jumpForce = 5f;
    public float jumpDecrease = 5f;
    public float maxJumpForce;
    public float dashSpace;
    public float jumpTimer;
    public float maxJumpTimer = 0.2f;
    public float jumpCheckTimer = 0.2f;
    public float maxJumpCheckTimer = 0.2f;

    public bool isCoyote = true;

    public bool isDashing = false;

    public bool isGrounded = true;

    public LayerMask groundLayers;

    public Transform groundCheck;
    public GameObject leftCheck;
    public GameObject rightCheck;

    //DASHING
    public float maxDashTimer;

    public float dashTimer;
    public int dashAmount;

    [HideInInspector] public Rigidbody2D rb;
    Animator ani;
    SpriteRenderer sprite;
    private ShadowForm shadowForm;
    float horizontalMovement;
    RaycastHit2D hit;
    public enum MoveState { idle, running, jumping, falling, dashing };
    public MoveState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shadowForm = GetComponent<ShadowForm>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        ShadowMove();
        CheckGrounded();
        PlayerJump();

        jumpCheckTimer -= Time.deltaTime;

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
    }

    private void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers) && jumpCheckTimer <= 0)
        {
            isGrounded = true;
        }
        else
        {
            if (jumpCheckTimer > 0)
            {
                isGrounded = false;
            }
            RemoveGrounded();
        }
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers);
    }

    private void RemoveGrounded()
    {
        isCoyote = true;
    }

    private void PlayerJump()
    {
        if (Input.GetButton("Jump"))
        {
            isGrounded = false;
            jumpCheckTimer = maxJumpCheckTimer;
        }

        //inside FixedUpdate
        if (Input.GetButton("Jump") && jumpTimer > 0 && jumpForce > 0)
        {
            Debug.Log("Jumping");
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpForce -= jumpDecrease; //or whatever amount
        }
        //inside Update
        if (isGrounded)
        {
            jumpForce = maxJumpForce; //go back to original power
            jumpTimer = maxJumpTimer;
        }
        else
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void ShadowMove()
    {
        if (!shadowForm.isInShadowForm) { return; }

        // Get input for horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // Set the velocity of the rigidbody
        if (Physics2D.Raycast(leftCheck.transform.position, -transform.up, 0.1f) && horizontalMovement < 0)
        {
            Debug.Log("Moving Left");
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        }
        
        if (Physics2D.Raycast(rightCheck.transform.position, -transform.up, 0.1f) && horizontalMovement > 0)
        {
            Debug.Log("Moving Right");
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        }


        if (!Physics2D.Raycast(rightCheck.transform.position, -transform.up, 0.1f) && rb.velocity.x > 0.05f)
        {
            Debug.Log("Can't Move Right");
            rb.velocity = Vector2.zero;
        }

        if (!Physics2D.Raycast(leftCheck.transform.position, -transform.up, 0.1f) && rb.velocity.x < 0.05f)
        {
            Debug.Log("Can't Move Left");
            rb.velocity = Vector2.zero;
        }

        UpdateAnimations();
    }

    void PlayerMove()
    {
        if (shadowForm.isInShadowForm) { return; }

        // Get input for horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        float speedSet = horizontalMovement * moveSpeed;

        // Set the velocity of the rigidbody

        if (rb.velocity.x <= maxSpeed && speedSet > 0)
        {
            rb.AddForce(new Vector2(speedSet, 0));
        }
        else if (rb.velocity.x >= -maxSpeed && speedSet < 0)
        {
            rb.AddForce(new Vector2(speedSet, 0));
        }

        if (dashTimer > 0)
        {
            Move();
        }
        else if (dashTimer < 0)
        {
            isDashing = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isDashing = true;
            Dash(dashSpace);
        }

        UpdateAnimations();
    }

    private void Move()
    {
        if (horizontalMovement < 0)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, rb.velocity.x - dashSpace, dashTimer), 0);
        }
        if (horizontalMovement > 0)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, rb.velocity.x + dashSpace, dashTimer), 0);
        }
    }

    void Dash(float dashSpace)
    {
        if (dashAmount >= 1 && rb.velocity.x != 0)
        {
            Debug.Log("Dashing (through the snow)");
            dashAmount -= 1;
            dashTimer = maxDashTimer;
        }
    }

    public void DashAdd(int dashIncrement)
    {
        if (dashAmount + dashIncrement >= 3)
        {
            dashAmount = 3;
        }
        else
        {
            dashAmount += dashIncrement;
        }
    }

    void UpdateAnimations()
    {
        if (!isDashing)
        {
            if (horizontalMovement > 0f)
            {
                state = MoveState.running;
                sprite.flipX = false;
            }
            else if (horizontalMovement < 0f)
            {
                state = MoveState.running;
                sprite.flipX = true;
            }
            else
            {
                state = MoveState.idle;
            }
        }
        else if (isDashing)
        {
            if (horizontalMovement > 0f)
            {
                state = MoveState.dashing;
                sprite.flipX = false;
            }
            else if (horizontalMovement < 0f)
            {
                state = MoveState.dashing;
                sprite.flipX = true;
            }
            else
            {
                state = MoveState.idle;
            }
        }

        if (rb.velocity.y > 0.01f)
        {
            state = MoveState.jumping;
        }
        else if (rb.velocity.y < -0.01f)
        {
            state = MoveState.falling;
        }

        ani.SetInteger("state", (int)state);
    }
}
