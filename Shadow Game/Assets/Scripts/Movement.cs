using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    public LayerMask groundLayers;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayers);

        float horizontalMovement = Input.GetAxis("Horizontal");

        // Get input for horizontal movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Set the velocity of the rigidbody
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
}
