using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashSpace;

    public bool isGrounded = true;

    public LayerMask groundLayers;

    public Transform groundCheck;
    public GameObject leftCheck;
    public GameObject rightCheck;

    public float maxDashTimer;

    public float dashTimer;
    Rigidbody2D rb;
    private ShadowForm shadowForm;
    float horizontalMovement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shadowForm = GetComponent<ShadowForm>();
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

        float horizontalMovement = Input.GetAxis("Horizontal");

        // Get input for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Set the velocity of the rigidbody
        if (Physics2D.Raycast(leftCheck.transform.position, -transform.up, 0.1f) && horizontalMovement < 0)
        {
            Debug.Log("Moving Left");
            rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        }
        else if (!Physics2D.Raycast(leftCheck.transform.position, -transform.up, 0.1f) && horizontalMovement < 0)
        {
            Debug.Log("Can't Move Left");
            rb.velocity = Vector2.zero;
        }
        if (Physics2D.Raycast(rightCheck.transform.position, -transform.up, 0.1f) && horizontalMovement > 0)
        {
            Debug.Log("Moving Right");
            rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        }
        else if (!Physics2D.Raycast(rightCheck.transform.position, -transform.up, 0.1f) && horizontalMovement > 0)
        {
            Debug.Log("Can't Move Right");
            rb.velocity = Vector2.zero;
        }
    }

    void PlayerMove()
    {
        if (shadowForm.isInShadowForm) { return; }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.35f, groundLayers);

        horizontalMovement = Input.GetAxis("Horizontal");

        // Get input for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Set the velocity of the rigidbody
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

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

        if (Input.GetMouseButtonDown(0))
        {
            Dash(dashSpace);
        }
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
}
