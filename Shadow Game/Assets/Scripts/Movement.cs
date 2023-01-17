using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpDecrease = 5f;
    [SerializeField] private float maxJumpForce;
    [SerializeField] private float jumpTimer;
    [SerializeField] private float maxJumpTimer = 0.2f;
    [SerializeField] private float jumpCheckTimer = 0.2f;
    [SerializeField] private float maxJumpCheckTimer = 0.2f;
    [SerializeField] private bool isCoyote = true;
    [SerializeField] private bool isGrounded = true;

    [Header("Dash")]
    [SerializeField] private float dashSpace;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool canDash = true;
    [SerializeField] private float maxDashTimer;
    [SerializeField] private float dashTimer;
    [SerializeField] private int dashAmount;

    [Header("Attack")]
    [SerializeField] private bool isAttacking = false;

    [Header("Ground Checks")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;
    public GameObject leftCheck;
    public GameObject rightCheck;

    private Rigidbody2D rb;
    private Animator ani;
    private SpriteRenderer sprite;
    private ShadowForm shadowForm;
    private float horizontalMovement;
    RaycastHit2D hit;
    public enum MoveState { idle, running, jumping, falling, dashing, shadow, attack };
    [SerializeField] private MoveState state;

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
        jumpCheckTimer -= Time.deltaTime;

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
        MoveInput();
    }

    private void MoveInput()
    {
        // Get input for horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(0))
        {
            isDashing = true;
            Dash(dashSpace);
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
        ShadowMove();
        CheckGrounded();
        PlayerJump();
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
            rb.AddForce(new Vector2(0f, jumpForce * (Time.fixedDeltaTime * 500)));
            jumpForce -= jumpDecrease; //or whatever amount
        }
        //inside Update
        if (isGrounded)
        {
            canDash = true;
            jumpForce = maxJumpForce; //go back to original power
            jumpTimer = maxJumpTimer;
        }
        else
        {
            jumpTimer -= Time.fixedDeltaTime;
        }
    }

    private void ShadowMove()
    {
        if (!shadowForm.isInShadowForm) { return; }

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

    private void PlayerMove()
    {
        if (shadowForm.isInShadowForm) { return; }

        float speedSet = horizontalMovement * moveSpeed * (Time.fixedDeltaTime * 500);

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

        UpdateAnimations();
    }

    private void Move()
    {
        if (horizontalMovement < 0)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, rb.velocity.x - dashSpace, dashTimer * (Time.fixedDeltaTime * 500)), 0);
        }
        if (horizontalMovement > 0)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, rb.velocity.x + dashSpace, dashTimer * (Time.fixedDeltaTime * 500)), 0);
        }
    }

    private void Dash(float dashSpace)
    {
        if (dashAmount >= 1 && rb.velocity.x != 0 && canDash)
        {
            Debug.Log("Dashing (through the snow)");
            dashAmount -= 1;
            canDash = false;
            dashTimer = maxDashTimer;
        }
    }

    private void DashAdd(int dashIncrement)
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

    public bool GetGrounded()
    {
        return isGrounded;
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

        if (isAttacking)
        {
            state = MoveState.attack;
        }

        if (shadowForm.isInShadowForm)
        {
            state = MoveState.shadow;
        }

        ani.SetInteger("state", (int)state);
    }
}
