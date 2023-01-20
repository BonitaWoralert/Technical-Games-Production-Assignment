using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy_Slime_AI : MonoBehaviour
{
    /// <summary>
    /// The script should make the AI to:
    ///     Patrol
    ///     Wunder (Sus)
    ///     Chase
    ///     Return (Go back to patrol pattern location)
    /// </summary>

    private bool isSpriteFlip;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Vector2 defaultVisionBoxPos;
    private Vector2 defaultAttackBoxPos;
    private Vector2 defaultAttackCollisionBoxOffsetX;
    public bool isPlayerSpotted;

    private float patrolStartPointX;
    [SerializeField] private float patrolStartPointY;

    [Header("Patrol Point/Location")]
    [Tooltip("The PosX of where the enemy should Patrol to")]
    [SerializeField] private float patrolEndPointX;
    [SerializeField] private float patrolEndPointOffsetX;
    [SerializeField] private float patrolEndPointY;

    [Tooltip("Keep this value around 0.5 - 3")]
    [SerializeField] private float patrolPointRange;

    [SerializeField] private Vector2 resetPosition;

    [Space(20)]

    [Header("Enemy Speed")]
    [Tooltip("Enemy patrol Move Speed")]
    [SerializeField] private float normalMovementSpeed;
    [Tooltip("Enemy Chase Speed Multiplier from normal Move Speed")]
    [SerializeField] private float chaseMovementMultiplier;
    [SerializeField] private float defaultTimeToMove;
    [SerializeField] private float movementForceY;
    [SerializeField] private float movementForceX;

    [Space(20)]

    [SerializeField] private Vector3 playerDistanceBuffer;

    private bool isMoveLeft;
    [HideInInspector]public bool isPatrolToMoveDirectionLeft;

    [Space(20)]

    [SerializeField] private Rigidbody2D rb;
    [Tooltip("Reference the vision box game object in the enemy's children")]
    [SerializeField] private GameObject visionBoxObject;
    [SerializeField] private GameObject attackBoxObject;
    [SerializeField] private BoxCollider2D attackBoxCollider;

    [SerializeField] private float attackRange;

    [Space(20)]

    [SerializeField] private float suspiciousValue;

    [Space(20)]

    [Header("Debuging Only")]
    [SerializeField] private AIState2 currentAIState2;
    private GameObject playerObject;
    private float checkTimer;
    [SerializeField] private float maxCheckTimer = 1f;
    [SerializeField] private Vector3 destination;
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;
    [SerializeField] private float timeToMove;
    public bool canMove;

    private Movement move;
    private Animator animator;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        defaultColor = spriteRenderer.color;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = transform.position.y;
        patrolEndPointX = patrolStartPointX + patrolEndPointOffsetX;
        resetPosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        playerObject = GameObject.Find("Player");
        canMove = true;
        isPlayerSpotted = false;
        destination = new Vector3(patrolEndPointX, patrolStartPointY, 0);

        timeToMove = defaultTimeToMove;

        if (patrolStartPointX < patrolEndPointX)
        {
            isMoveLeft = false;
            isPatrolToMoveDirectionLeft = false;
        }
        else
        {
            isMoveLeft = true;
            isPatrolToMoveDirectionLeft = true;
        }

        currentAIState2 = AIState2.PATROLTO;
    }

    void Update()
    {
        switch (currentAIState2)
        {
            case AIState2.NONE:
                break;
            case AIState2.PATROLTO:
                PatrolTo();
                break;
            case AIState2.PATROLBACK:
                PatrolBack();
                break;
            case AIState2.RETURNTOPATROL:
                ReturnToPatrol();
                break;
            default:
                break;
        }

        MoveTimer();
        SlimeAnimationCheck();
        VelocityInputTest();

        if(canMove)
        {
            if (destination.x - gameObject.transform.position.x < 0f)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }
    }

    private void SlimeAnimationCheck()
    {
        //If slime Velocity y is more than 0, play jumping animation.
        if (velocityY != 0)
        {
            if (velocityY > 0.01f || velocityY < -0.01f)
            {
                animator.SetBool("isFalling", false);
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }
        else
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
 

        //If slime Velocity y is less than or equal to 0, play falling animation.

        //If slime movement is in Cooldown, play stay still animation.
    }

    private void VelocityInputTest()
    {
        //Remove this function when test is finished.
        if(Input.GetKeyDown("e"))
        {
            //rb.AddForce(new Vector2(0, movementForceY));
            rb.velocity += new Vector2(movementForceX, movementForceY);
        }

        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;
    }

    private void CheckTimer()
    {
        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0f)
        {
            checkTimer = maxCheckTimer;
            FindCheck();
        }
    }

    private void FindCheck()
    {
        move = playerObject.GetComponent<Movement>();
        if (Vector2.Distance(gameObject.transform.position, move.leftCheck.transform.position) < Vector2.Distance(gameObject.transform.position, move.rightCheck.transform.position))
        {
            ChangeDestination(move.leftCheck.transform.position - playerDistanceBuffer);
        }
        else
        {
            ChangeDestination(move.rightCheck.transform.position + playerDistanceBuffer);
        }
    }

    private void ChangeDestination(Vector3 newDestination)
    {
        destination = newDestination;
    }

    //Going to the End Point.
    private void PatrolTo()
    {
        if(patrolEndPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolEndPointX + patrolPointRange)
        {
            destination = new Vector3(patrolStartPointX, patrolStartPointY, 0);
            //canMove = true;
            //Enemy is in range of patrolEndPoint. Start going back.
            currentAIState2 = AIState2.PATROLBACK;
       
            //Change Movement Direction
            isMoveLeft = !isPatrolToMoveDirectionLeft;
        }
    }

    //Going Back to the Start Point.
    private void PatrolBack()
    {
        if(patrolStartPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolStartPointX + patrolPointRange)
        {
            destination = new Vector3(patrolEndPointX, patrolStartPointY, 0);
            //canMove = true;
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState2 = AIState2.PATROLTO;

            //Change Movement Direction
            isMoveLeft = isPatrolToMoveDirectionLeft;
        }
    }

    //This function makes the enemy go back to the their starting spawn location.
    private void ReturnToPatrol()
    {
        destination = new Vector3(patrolEndPointX, patrolStartPointY, 0);

        if (!(patrolStartPointY - 0.3f < rb.transform.position.y || rb.transform.position.y > patrolStartPointY + 0.3f))
        {
            canMove = false;
            StartCoroutine(Telp());
        }
        else
        { 
            //This if statement makes sure the enemy AI switch state to patrol mode when its in range of the starting patrol point.
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState2 = AIState2.PATROLTO;
        }

        canMove = true;
    }

    private IEnumerator Telp()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = resetPosition;
    }

    private void MoveLeft()
    {
        if(currentAIState2 != AIState2.NONE)
        {
            rb.velocity += new Vector2(-movementForceX, movementForceY);
            velocityX = rb.velocity.x;
            velocityY = rb.velocity.y;
            canMove = false;
        }
    }

    private void MoveRight()
    {
        if(currentAIState2 != AIState2.NONE)
        {   
            rb.velocity += new Vector2(movementForceX, movementForceY);
            velocityX = rb.velocity.x;
            velocityY = rb.velocity.y;
            canMove = false;
        }
    }

    private void MoveTimer()
    {
        //This timer calculates the time the slime has to stay on the ground before moving again.
        if(velocityY <= 0)
        {
            //Start the Timer.
            timeToMove -= Time.deltaTime;
        }

        if(timeToMove <= 0)
        {
            //Slime can now move.
            canMove = true;
            timeToMove = defaultTimeToMove;
        }
    }
    
    public void ChangeAIState2(AIState2 newAIState2)
    {
        currentAIState2 = newAIState2;
    }

    public AIState2 GetLastAIState2()
    {
        return currentAIState2;
    }

    public float GetSuspiciousValue()
    {
        return suspiciousValue;
    }

    public void SetSuspiciousValue(float newSuspiciousValue)
    {
        suspiciousValue = newSuspiciousValue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, new Vector2(patrolEndPointX, gameObject.transform.position.y));
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}


