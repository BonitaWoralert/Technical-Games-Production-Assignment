using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [Range(0.1f, 15)]
    [SerializeField] private float speedDecrement = 6.0f;

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
    [SerializeField] private int maxDashLevel;
    [SerializeField] private int maxRegenDashLevel;
    public float currentDashPower;
    [SerializeField] private float dashRegen;

    [Header("Attack")]
    [SerializeField] private bool isAttacking = false;

    [Header("Ground Checks")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;
    public GameObject leftCheck;
    public GameObject rightCheck;

    [Header("Sounds")]
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource jumpSound;

    private Rigidbody2D rb;
    private Animator ani;
    private SpriteRenderer sprite;
    private ShadowForm shadowForm;
    private PlayerStats playerStats;
    private float horizontalMovement;
    public enum MoveState { idle, running, jumping, falling, dashing, shadow, attack };
    [SerializeField] private MoveState state;
    private TrailRenderer trailRenderer;
    [SerializeField] bool facingRight;
    private bool dashGravity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shadowForm = GetComponent<ShadowForm>();
        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpCheckTimer > 0)
        {
            jumpCheckTimer -= Time.deltaTime;
        }
        dashTimer -= Time.deltaTime;
        

        DashMovement();

        trailRenderer.enabled = (dashTimer + 0.25f > 0);

        Resistance();
    }
    private void FixedUpdate()
    {
        PlayerMove();
        CheckGrounded();
        PlayerJump();
    }

    private void Resistance()
    {
        if (isGrounded && dashTimer <= -0.1)
        {
            rb.AddForce(new Vector2(-rb.velocity.x * (1/ speedDecrement), 0));
        }
    }

    private void DashMovement()
    {
        

        // Get input for horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (currentDashPower < maxRegenDashLevel)
        {
            currentDashPower += Time.deltaTime * dashRegen;
        }

        playerStats.currentDashLevel = (int)MathF.Floor(currentDashPower);

        if (Input.GetButtonDown("Dash") && !shadowForm.isInShadowForm)
        {
            dashSound.Play();
            isDashing = true;
            Dash(dashSpace);
        }
    }

    public void CheckGrounded()
    {
        //Debug.Log(Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers));
        Physics2D.queriesHitTriggers = false;
        if (Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers) && jumpCheckTimer <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
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
        if (shadowForm.isInShadowForm)
        {
            return;
        }

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
            jumpForce -= jumpDecrease; //Or Whatever amount
        }
        //Inside Update
        if (isGrounded)
        {
            canDash = true;
            jumpForce = maxJumpForce; //Go Back to Original Power
            jumpTimer = maxJumpTimer;
        }
        else
        {
            if (jumpCheckTimer > 0)
            {
                jumpTimer -= Time.fixedDeltaTime;
            }
        }
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
        if (horizontalMovement == 0)
        {
            if (facingRight == true)
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, rb.velocity.x + dashSpace, dashTimer * (Time.fixedDeltaTime * 500)), 0);
            }
            if (facingRight == false)
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, rb.velocity.x - dashSpace, dashTimer * (Time.fixedDeltaTime * 500)), 0);
            }
        }
    }

    private void Dash(float dashSpace)
    {
        if (canDash && currentDashPower > 1)
        {
            Debug.Log("Dashing (through the snow)");
            canDash = false;
            currentDashPower -= 1;
            dashTimer = maxDashTimer;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
            Invoke("ChangeGravity", 0.3f);
        }
    }

    private void ChangeGravity()
    {
        rb.gravityScale = 1.7f;
    }

    public void DashAdd(int dashIncrement)
    {
        if (currentDashPower + dashIncrement >= maxDashLevel)
        {
            currentDashPower = maxDashLevel;
        }
        else
        {
            currentDashPower += dashIncrement;
        }
    }

    public void SetGrounded(bool newState)
    {
        isGrounded = newState;
    }

    public bool GetGrounded()
    {
        return isGrounded;
    }

    public void UpdateAnimations()
    {
        if (!isDashing)
        {
            if (horizontalMovement > 0f)
            {
                state = MoveState.running;
                sprite.flipX = false;
                facingRight = true;
            }
            else if (horizontalMovement < 0f)
            {
                state = MoveState.running;
                sprite.flipX = true;
                facingRight = false;
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
