using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashSpace;
    public bool isDashing = false;

    public bool isGrounded = true;

    public LayerMask groundLayers;

    public Transform groundCheck;
    public GameObject leftCheck;
    public GameObject rightCheck;

    public float maxDashTimer;

    public float dashTimer;
    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer sprite;
    private ShadowForm shadowForm;
    float horizontalMovement;

    enum MoveState { idle, running, jumping, falling, dashing, shadow };
    MoveState state;

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
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
    }

    private void ShadowMove()
    {
        if (!shadowForm.isInShadowForm) { return; }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers);

        // Get input for horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // Set the velocity of the rigidbody
        if (Physics2D.Raycast(leftCheck.transform.position, -transform.up, 0.1f) && horizontalMovement < 0)
        {
            Debug.Log("Moving Left");
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        }
        else if (!Physics2D.Raycast(leftCheck.transform.position, -transform.up, 0.1f) && horizontalMovement < 0)
        {
            Debug.Log("Can't Move Left");
            rb.velocity = Vector2.zero;
        }
        if (Physics2D.Raycast(rightCheck.transform.position, -transform.up, 0.1f) && horizontalMovement > 0)
        {
            Debug.Log("Moving Right");
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        }
        else if (!Physics2D.Raycast(rightCheck.transform.position, -transform.up, 0.1f) && horizontalMovement > 0)
        {
            Debug.Log("Can't Move Right");
            rb.velocity = Vector2.zero;
        }

        UpdateAnimations();
    }

    void PlayerMove()
    {
        if (shadowForm.isInShadowForm) { return; }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers);

        // Get input for horizontal movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // Set the velocity of the rigidbody
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !shadowForm.isInShadowForm)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (dashTimer > 0)
        {
            Move();
        }
        else if (dashTimer < 0)
        {
            isDashing = false;
        }

        if (maxDashTimer <= 0)
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
        Debug.Log("Dashing (through the snow)");
        dashTimer = maxDashTimer;
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

        if (shadowForm.isInShadowForm)
        {
            state = MoveState.shadow;
        }

        ani.SetInteger("state", (int)state);
    }
}
