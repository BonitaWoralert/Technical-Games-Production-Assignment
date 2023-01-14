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
    ///     Wonder (Suspicious)
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

    private bool isMoveLeft;
    [HideInInspector]public bool canMove;
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

    [Header("Enemy Suspicious and Chase Values")]
    [Tooltip("How fast the enemy gets Suspicious")]
    [SerializeField] private float suspiciousFillUpSpeed;
    [SerializeField] private float suspiciousDrainSpeed;
    [Tooltip("Suspicious Value for the enemy to be Suspicious")]
    [SerializeField] private float suspiciousThreshold;
    [Tooltip("The maximum suspicious value the enemy can have, the bigger the value, the longer the chase state can be based on the chaseThreshold value")]
    [SerializeField] private float suspiciousValueMax;
    [Tooltip("Suspicious Value for the enemy to be Chasing")]
    [SerializeField] private float chaseThreshold;

    [Space(20)]

    [Header("Debuging Only")]
    [SerializeField] private AIState2 currentAIState2;
    private GameObject playerObject;
    private float checkTimer;
    [SerializeField] private float maxCheckTimer = 1f;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Vector3 playerDistanceBuffer;
    [SerializeField] private float movementForceY;
    [SerializeField] private float movementForceX;
    [SerializeField] private float velocityX;
    [SerializeField] private float velocityY;

    private Movement move;
    private Animator animator;

    [Space(20)]

    private bool isAttacking;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        defaultColor = spriteRenderer.color;
        //defaultVisionBoxPos = visionBoxObject.transform.localPosition;
        //defaultAttackBoxPos = attackBoxObject.transform.localPosition;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = transform.position.y;
        patrolEndPointX = patrolStartPointX + patrolEndPointOffsetX;
        resetPosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        playerObject = GameObject.Find("Player");
        canMove = true;
        isPlayerSpotted = false;
        destination = new Vector3(patrolEndPointX, patrolStartPointY, 0);

        //attackBoxCollider = attackBoxObject.GetComponent<BoxCollider2D>();
        //defaultAttackCollisionBoxOffsetX = attackBoxCollider.offset;
        isAttacking = false;

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
            case AIState2.SUSPICIOUS:
                Suspicious();
                break;
            case AIState2.CHASE:
                Chase();
                break;
            case AIState2.ATTACK:
                Attack();
                break;
            case AIState2.RETURNTOPATROL:
                ReturnToPatrol();
                break;
            default:
                break;
        }

        //if(isPlayerSpotted)
        //{
        //    if(suspiciousValue > suspiciousValueMax)
        //    {
        //        suspiciousValue = suspiciousValueMax;
        //    }
        //    else
        //    {
        //        suspiciousValue += suspiciousFillUpSpeed * Time.deltaTime;
        //    }
        //
        //}
        //else
        //{
        //    if(suspiciousValue < 0f)
        //    {
        //        suspiciousValue = 0f;
        //    }
        //    else
        //    {
        //        suspiciousValue -= suspiciousDrainSpeed * Time.deltaTime;
        //    }
        //
        //}

        SlimeAnimationCheck();
        VelocityInputTest();

        if(canMove)
        {
            //if (destination.x - gameObject.transform.position.x < 0f)
            //{
            //    MoveLeft();
            //}
            //else
            //{
            //    MoveRight();
            //}
        }

        //if(chaseThreshold > suspiciousValue && suspiciousValue > suspiciousThreshold)
        //{
        //    ChangeAIState2(AIState2.SUSPICIOUS);
        //}

    }

    private void SlimeAnimationCheck()
    {
        //If slime Velocity y is more than 0, play jumping animation.

        //If slime Velocity y is less than or equal to 0, play falling animation.

        //If slime movement is in Cooldown, play stay still animation.
    }

    private void VelocityInputTest()
    {
        //Remove this function when test is finished.
        if(Input.GetKeyDown("e"))
        {
            //rb.AddForce(new Vector2(0, movementForceY));
            rb.velocity += new Vector2(0, movementForceY);
            Debug.Log("FORCE ADDED!");
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
            canMove = true;
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
            canMove = true;
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState2 = AIState2.PATROLTO;

            //Change Movement Direction
            isMoveLeft = isPatrolToMoveDirectionLeft;
        }
    }

    //Chase the player.
    private void Chase()
    {
        CheckTimer();
        canMove = true;
        if (transform.position.x <= playerObject.transform.position.x)
        {
            isMoveLeft = false;
        }
        else
        {
            isMoveLeft = true;
        }

        if(-attackRange < destination.x - gameObject.transform.position.x && destination.x - gameObject.transform.position.x < attackRange)
        {
            if(isAttacking == false)
            {
                isAttacking = true;
                Attack();
            }
        }
    }

    private void Attack()
    {
        animator.SetTrigger("ghoulAttack");
    }

    private void AttackStart()
    {
        canMove = false;
        attackBoxCollider.enabled = true;
    }

    private void AttackFinished()
    {
        canMove = true;
        attackBoxCollider.enabled = false;
    }

    //This function makes the enemy go back to the their starting spawn location.
    private void ReturnToPatrol()
    {
        destination = new Vector3(patrolEndPointX, patrolStartPointY, 0);

        if (!(patrolStartPointY - 0.3f < rb.transform.position.y || rb.transform.position.y > patrolStartPointY + 0.3f))
        {
            canMove = false;
            //rb.MovePosition(new Vector2(rb.transform.position.x, patrolStartPointY));
            StartCoroutine(Telp());

            //rb.transform.position = new Vector2(rb.transform.position.x, patrolStartPointY);
            //rb.transform.position = new Vector2(rb.transform.position.x, patrolStartPointY);
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

    //Enemy stays still.
    private void Suspicious()
    {
        //AI does not move, until suspicious bar fills up.
        if (suspiciousValue > chaseThreshold)
        {
            animator.SetBool("isSuspicious", false);
            canMove = true;
            currentAIState2 = AIState2.CHASE;
        }
        else if (suspiciousThreshold < suspiciousValue && suspiciousValue < chaseThreshold)
        {
            animator.SetBool("isSuspicious", true);
            canMove = false;
        }

        if (suspiciousValue == 0f)
        {
            animator.SetBool("isSuspicious", false);
            canMove = true;
            ChangeAIState2(AIState2.RETURNTOPATROL);
        }
    }

    private void MoveLeft()
    {
        if(currentAIState2 != AIState2.NONE)
        {
            if(currentAIState2 == AIState2.CHASE)
            {
                transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * chaseMovementMultiplier * Time.deltaTime), rb.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * Time.deltaTime), rb.position.y);
            }
            //HERE!!
            spriteRenderer.flipX = true;
            //visionBoxObject.transform.localPosition = new Vector3(defaultVisionBoxPos.x * -1, defaultVisionBoxPos.y);
            //attackBoxObject.transform.localPosition = new Vector3(defaultAttackBoxPos.x * -1, defaultAttackBoxPos.y);
            //attackBoxCollider.offset = defaultAttackCollisionBoxOffsetX * -1;
            //visionBoxObject.transform.position = new Vector3(-defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
            //spriteRenderer.color = defaultColor;
        }
    }

    private void MoveRight()
    {
        if(currentAIState2 != AIState2.NONE)
        {
            if(currentAIState2 == AIState2.CHASE)
            {
                transform.position = new Vector2(transform.position.x + (normalMovementSpeed * chaseMovementMultiplier * Time.deltaTime), rb.position.y);
            }
            else
            {
                //transform.position = new Vector2(transform.position.x + (normalMovementSpeed * Time.deltaTime), rb.position.y);
                //rb.MovePosition(new Vector2(transform.position.x + (normalMovementSpeed * Time.deltaTime)));
            }
            if (currentAIState2 != AIState2.CHASE)
            {
                //spriteRenderer.flipX = false;
            }
            else
            {
                //if (currentAIState2 == AIState2.CHASE && destination == move.leftCheck.transform.position)
                //{
                //    //spriteRenderer.flipX = true;
                //}
                //else if (currentAIState2 == AIState2.CHASE && destination == move.rightCheck.transform.position)
                //{
                //    //spriteRenderer.flipX = false;
                //}
            }
            //visionBoxObject.transform.localPosition = defaultVisionBoxPos;
            //attackBoxObject.transform.localPosition = defaultAttackBoxPos;
            //attackBoxCollider.offset = defaultAttackCollisionBoxOffsetX;
            //visionBoxObject.transform.position = new Vector3(defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
            //spriteRenderer.color = Color.yellow;
            spriteRenderer.flipX = false;
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



