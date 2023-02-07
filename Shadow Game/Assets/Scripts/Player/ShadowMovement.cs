using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    [Header("Checks")]
    [SerializeField] private GameObject LeftCheck;
    [SerializeField] private GameObject UpCheck;
    [SerializeField] private GameObject RightCheck;
    [SerializeField] private GameObject DownCheck;
    [Space(10)]
    public GameObject leftSideCheck;
    public GameObject rightSideCheck;
    public GameObject CentreRotatePoint;

    private bool LeftCheckBool;
    private bool UpCheckBool;
    private bool RightCheckBool;
    private bool DownCheckBool;

    [Header("Values")]
    [SerializeField] private float checkDistance;
    private ShadowForm shadowForm;
    private Movement movement;

    private Rigidbody2D rb;
    public enum State
    {
        Normal,
        UpsideDown,
        Right,
        Left
    }

    [HideInInspector] public State state;
    float vertical;
    float horizontal;
    public float moveSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float maxSpeed;
    [SerializeField] private LayerMask lm;
    [SerializeField] private float changeTimer;
    [SerializeField] private float maxChangeTimer;

    void Start()
    {
        state = State.Normal;
        shadowForm = GetComponent<ShadowForm>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (shadowForm.isInShadowForm)
        {
            Check();
            RotationLogic();
            GravityManipulation();
            ShadowMove();
        }
        if (changeTimer > 0)
        {
            changeTimer -= Time.deltaTime;
        }
    }

    private void GravityManipulation()
    {
        switch (state)
        {
            case State.Normal:
                rb.AddForce(new Vector2(0, -gravity));
                break;
            case State.Left:
                rb.AddForce(new Vector2(-gravity, 0));
                break;
            case State.Right:
                rb.AddForce(new Vector2(gravity, 0));
                break;
            case State.UpsideDown:
                rb.AddForce(new Vector2(0, gravity));
                break;
        }
    }

    private void RotationLogic()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        if (state == State.Normal && changeTimer <= 0)
        {
            if (vertical > 0)
            {
                if (LeftCheckBool == true)
                {
                    RotateRight();
                    state = State.Left;
                }
                else if (RightCheckBool == true)
                {
                    RotateLeft();
                    state = State.Right;
                }
            }
        }
        if (state == State.Left && changeTimer <= 0)
        {
            if (horizontal > 0)
            {
                if (LeftCheckBool == true)
                {
                    RotateRight();
                    state = State.UpsideDown;
                }
                else if (RightCheckBool == true)
                {
                    RotateLeft();
                    state = State.Normal;
                }
            }
        }
        if (state == State.UpsideDown && changeTimer <= 0)
        {
            if (vertical < 0)
            {
                if (LeftCheckBool == true)
                {
                    RotateRight();
                    state = State.Right;
                }
                else if (RightCheckBool == true)
                {
                    RotateLeft();
                    state = State.Left;
                }
            }
        }
        if (state == State.Right && changeTimer <= 0)
        {
            if (horizontal < 0)
            {
                if (LeftCheckBool == true)
                {
                    RotateRight();
                    state = State.Normal;
                }
                else if (RightCheckBool == true)
                {
                    RotateLeft();
                    state = State.UpsideDown;
                }
            }
        }
    }

    void RotateLeft()
    {
        //Turn Left
        Debug.Log("Turned Left");
        changeTimer = maxChangeTimer;
        transform.RotateAround(CentreRotatePoint.transform.position, new Vector3(0, 0, 1), 90);
    }

    void RotateRight()
    {
        //Turn Right
        Debug.Log("Turned Right");
        changeTimer = maxChangeTimer;
        transform.RotateAround(CentreRotatePoint.transform.position, new Vector3(0, 0, 1), -90);
    }

    private void Check()
    {
        //Basic Checks to Determine Interactions
        Physics2D.queriesHitTriggers = false;
        LeftCheckBool = Physics2D.Raycast(LeftCheck.transform.position, -transform.right, checkDistance, lm);
        RightCheckBool = Physics2D.Raycast(RightCheck.transform.position, transform.right, checkDistance, lm);
        DownCheckBool = Physics2D.Raycast(DownCheck.transform.position, -transform.up, checkDistance, lm);
    }

    private void ShadowMove()
    {
        // Set the velocity of the rigidbody
        //It mobes
        switch (state)
        {
            case (State.Normal):
                //Left Movement for Normal State
                if (Physics2D.Raycast(leftSideCheck.transform.position, -transform.up, 0.1f) && horizontal < 0)
                {
                    if (rb.velocity.x > -maxSpeed && horizontal < 0)
                    {
                        rb.AddForce(new Vector2(-moveSpeed, 0));
                    }
                }
                //Right Movement for Normal State
                if (Physics2D.Raycast(rightSideCheck.transform.position, -transform.up, 0.1f) && horizontal > 0)
                {
                    if (rb.velocity.x < maxSpeed && horizontal > 0)
                    {
                        rb.AddForce(new Vector2(moveSpeed, 0));
                    }
                }
                break;
            case (State.Left):
                if (Physics2D.Raycast(leftSideCheck.transform.position, -transform.up, 0.1f) && vertical > 0)
                {
                    if (rb.velocity.y < maxSpeed && vertical > 0)
                    {
                        rb.AddForce(new Vector2(0, moveSpeed));
                    }
                }

                if (Physics2D.Raycast(rightSideCheck.transform.position, -transform.up, 0.1f) && vertical < 0)
                {
                    if (rb.velocity.y > -maxSpeed && vertical < 0)
                    {
                        rb.AddForce(new Vector2(0, -moveSpeed));
                    }
                }
                break;
            case (State.UpsideDown):
                if (Physics2D.Raycast(leftSideCheck.transform.position, -transform.up, 0.1f) && horizontal > 0)
                {
                    if (rb.velocity.x < maxSpeed)
                    {
                        rb.AddForce(new Vector2(moveSpeed, 0));
                    }
                }

                if (Physics2D.Raycast(rightSideCheck.transform.position, -transform.up, 0.1f) && horizontal < 0)
                {
                    if (rb.velocity.x > -maxSpeed)
                    {
                        rb.AddForce(new Vector2(-moveSpeed, 0));
                    }
                }
                break;
            case (State.Right):
                if (Physics2D.Raycast(rightSideCheck.transform.position, -transform.up, 0.1f) && vertical > 0)
                {
                    if (rb.velocity.y < maxSpeed && vertical > 0)
                    {
                        rb.AddForce(new Vector2(0, moveSpeed));
                    }
                }

                if (Physics2D.Raycast(leftSideCheck.transform.position, -transform.up, 0.1f) && vertical < 0)
                {
                    if (rb.velocity.y > -maxSpeed && vertical < 0)
                    {
                        rb.AddForce(new Vector2(0, -moveSpeed));
                    }
                }
                break;

        }
        if (!Physics2D.Raycast(DownCheck.transform.position, -transform.up, 0.1f))
        {
            rb.velocity = Vector2.zero;
        }



        movement.UpdateAnimations();
    }

    public void Refresh()
    {
        state = State.Normal;
        gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
    }
}