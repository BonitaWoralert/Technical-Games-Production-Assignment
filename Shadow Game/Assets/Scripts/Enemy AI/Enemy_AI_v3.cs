using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI_v3 : MonoBehaviour
{
    //This script attempts to use A* Pathfinding for the AI.

    [SerializeField] private Transform target;
    [SerializeField] private float speed; //200f //400f
    [SerializeField] private float nextWayPointDistance; //1f
    [SerializeField] private float pathUpdateSpeed; //0.5f
    [SerializeField] private float jumpStrength;
    [SerializeField] private float jumpThreshold;
    [SerializeField] private float jumpTimer;
    [SerializeField] private float defautJumpTimer;
    [SerializeField] private bool isJumping;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    //private Vector2 direction;

    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField] private Transform enemySprite;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = defautJumpTimer;

        InvokeRepeating("UpdatePath", 1f, pathUpdateSpeed);
    }

    void UpdatePath()
    {
        //Make sure that the enemy is not jumping.
        //Make sure that the seeker is NOT currently calculating a new path.
        if (seeker.IsDone())
        {
            //Generate new Path.
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }

    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            Jump();
        }
        JumpTimerUpdate();
    }

    void FixedUpdate()
    {
        if(path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 moveForce = direction * speed * Time.fixedDeltaTime;
        Vector2 moveForceX = new Vector2(direction.x * speed * Time.fixedDeltaTime, 0f);
        Vector2 moveForceSecond = direction * speed;

        if(isJumping == true)
        {
            rb.AddForce(moveForceX);
        }
        else
        {
            //Moving Left or Right.
            rb.AddForce(moveForce);
        }

        if(moveForce.y > jumpThreshold)
        {
            Jump();
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (moveForce.x >= 0.01f)
        {
            enemySprite.localScale = new Vector3(5f, 5f, 5f);
        }
        else if (moveForce.x <= -0.01f)
        {
            enemySprite.localScale = new Vector3(-5f, 5f, 5f);
        }
    }

    private void Jump()
    {
        if(isJumping == false)
        {
            rb.velocity += Vector2.up * jumpStrength;
            isJumping = true;
        }
    }

    private void JumpTimerUpdate()
    {
        if(isJumping == true && jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpTimer = defautJumpTimer;
        }
    }

}
