using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Experimental.XR.Interaction;

public class Enemy_AI_v3 : MonoBehaviour
{
    //This script attempts to use A* Pathfinding for the AI.

    [SerializeField] private Transform target;
    [SerializeField] private Transform enemyVisionBox;
    [SerializeField] private GameObject enemySpriteGameObject;
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private float speed; //200f //400f
    [SerializeField] private float moveSpeed; //200f //400f
    [SerializeField] private float nextWayPointDistance; //1f
    [SerializeField] private float pathUpdateSpeed; //0.5f
    [SerializeField] private float shortJumpStrength;
    [SerializeField] private float longJumpStrength;
    [SerializeField] private float shortJumpThreshold;
    [SerializeField] private float longJumpThreshold;
    [SerializeField] private float jumpTimer;
    [SerializeField] private float defautJumpTimer;
    [SerializeField] private bool isJumping;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;
    private Vector3 defaultSpriteSize;
    private Vector3 defaultVisionBoxPos;
    private bool previousDirection = false;
    [SerializeField] private Vector2 additionalSpeed;
    [SerializeField] private Vector2 directionDebug;
    [SerializeField] private Vector2 distanceVectorDebug;
    [SerializeField] private float distanceDebug;
    [SerializeField] private bool canShortJump;
    private GameObject playerGameObject;

    Seeker seeker;
    Rigidbody2D rb;

    bool canSpriteFlip;
    private bool isShortJump;

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        target = playerGameObject.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        additionalSpeed = new Vector2(5f, 0f);
        //jumpTimer = defautJumpTimer;
        defaultSpriteSize = transform.localScale / 6;
        defaultVisionBoxPos = enemyVisionBox.transform.localPosition;
        canSpriteFlip = true;

        InvokeRepeating("UpdatePath", 0.2f, pathUpdateSpeed);
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
            LongJump();
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
        directionDebug = direction;
        Vector2 moveForce = direction * speed * Time.fixedDeltaTime;
        Vector2 moveForceX = new Vector2(direction.x * speed * Time.fixedDeltaTime, 0f);
        Vector2 moveForceSecond = direction * speed;

        //if(isJumping == true)
        //{
        //    rb.AddForce(moveForceX);
        //    //Debug.Log("moveForceX");
        //}
        //else
        //{
        //    rb.AddForce(moveForce + additionalSpeed);
        //}

        if (direction.x > 0.01)
        {
            MoveRight();
            previousDirection = true;
        }
        else if (direction.x < -0.01)
        {
            MoveLeft();
            previousDirection = false;
        }
        else
        {
            if (previousDirection == true)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        //In some levels, short jumps are not necessary.
        if(canShortJump == true)
        {
            if (direction.y > shortJumpThreshold && direction.y < longJumpThreshold)
            {
                ShortJump();
            }
            else if (direction.y > longJumpThreshold)
            {
                LongJump();
            }
        }
        else
        {
            if (direction.y > longJumpThreshold)
            {
                LongJump();
            }
        }


        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        distanceVectorDebug = new Vector2(rb.position.x - path.vectorPath[currentWayPoint].x, rb.position.y - path.vectorPath[currentWayPoint].y);
        distanceDebug = distance;

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (moveForce.x >= 2f)
        {
            enemySpriteRenderer.flipX = false;
            //enemyVisionBox.transform.localPosition = defaultVisionBoxPos;
        }
        else if (moveForce.x <= -2f)
        {
            enemySpriteRenderer.flipX = true;
        }
        //else
        //{
        //    enemySpriteRenderer.flipX = false;
        //}
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    private void ShortJump()
    {
        if (isJumping == false)
        {
            rb.velocity += Vector2.up * shortJumpStrength;
            isJumping = true;
            isShortJump = true;
        }
    }

    private void LongJump()
    {
        if(isJumping == false)
        {
            rb.velocity += Vector2.up * longJumpStrength;
            isJumping = true;
            isShortJump = false;
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
            if(isShortJump == true)
            {
                jumpTimer = defautJumpTimer * 0.5f;
                isShortJump = false;
            }
            else
            {
                jumpTimer = defautJumpTimer;
            }
        }
    }

}
