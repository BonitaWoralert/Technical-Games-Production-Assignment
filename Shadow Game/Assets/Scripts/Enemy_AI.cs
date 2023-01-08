using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
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
    public bool isPlayerSpotted;

    private float patrolStartPointX;
    [SerializeField] private float patrolStartPointY;

    [Header("Patrol Point/Location")]
    [Tooltip("The PosX of where the enemy should Patrol to")]
    [SerializeField] private float patrolEndPointX;
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
    [SerializeField] private AIState currentAIState;
    private GameObject playerObject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        defaultVisionBoxPos = visionBoxObject.transform.localPosition;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = transform.position.y;
        resetPosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        playerObject = GameObject.Find("Player");
        canMove = true;
        isPlayerSpotted = false;

        //suspiciousValue = 0f;
        //suspiciousFillUpSpeed = 30f;
        //suspiciousDrainSpeed = 15f;
        //suspiciousThreshold = 50f;
        //suspiciousValueMax = 120f;
        //
        //chaseThreshold = 100f;

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

        currentAIState = AIState.PATROLTO;
    }

    void Update()
    {
        switch (currentAIState)
        {
            case AIState.NONE:
                break;
            case AIState.PATROLTO:
                PatrolTo();
                break;
            case AIState.PATROLBACK:
                PatrolBack();
                break;
            case AIState.SUSPICIOUS:
                Suspicious();
                break;
            case AIState.CHASE:
                Chase();
                break;
            case AIState.RETURNTOPATROL:
                ReturnToPatrol();
                break;
            default:
                break;
        }

        if(isPlayerSpotted)
        {
            //canMove = false;
            if(suspiciousValue > suspiciousValueMax)
            {
                suspiciousValue = suspiciousValueMax;
            }
            else
            {
                suspiciousValue += suspiciousFillUpSpeed * Time.deltaTime;
            }

        }
        else
        {
            if(suspiciousValue < 0f)
            {
                suspiciousValue = 0f;
            }
            else
            {
                suspiciousValue -= suspiciousDrainSpeed * Time.deltaTime;
            }

        }

        if(canMove)
        {
            if (isMoveLeft == true)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }

        if(chaseThreshold > suspiciousValue && suspiciousValue > suspiciousThreshold)
        {
            ChangeAIState(AIState.SUSPICIOUS);
        }
    }

    //Going to the End Point.
    private void PatrolTo()
    {
        if(patrolEndPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolEndPointX + patrolPointRange)
        {
            canMove = true;
            //Enemy is in range of patrolEndPoint. Start going back.
            currentAIState = AIState.PATROLBACK;
       
            //Change Movement Direction
            isMoveLeft = !isPatrolToMoveDirectionLeft;
        }
    }

    //Going Back to the Start Point.
    private void PatrolBack()
    {
        if(patrolStartPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolStartPointX + patrolPointRange)
        {
            canMove = true;
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState = AIState.PATROLTO;

            //Change Movement Direction
            isMoveLeft = isPatrolToMoveDirectionLeft;
        }
    }

    //Chase the player.
    private void Chase()
    {
        if(transform.position.x <= playerObject.transform.position.x)
        {
            isMoveLeft = false;
        }
        else
        {
            isMoveLeft = true;
        }
    }

    //This function makes the enemy go back to the their starting spawn location.
    private void ReturnToPatrol()
    {
        if(transform.position.x <= patrolStartPointX)
        {
            isMoveLeft = false;
        }
        else
        {
            isMoveLeft = true;
        }

        //This if statement makes sure the enemy AI switch state to patrol mode when its in range of the starting patrol point.
        if (patrolStartPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolStartPointX + patrolPointRange)
        {
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState = AIState.PATROLTO;

            //Makes sure to move in the correct direction
            if (patrolStartPointX < patrolEndPointX)
            {
                isMoveLeft = false;
            }
            else
            {
                isMoveLeft = true;
            }
        }

        if(!(patrolStartPointY - 0.3f < rb.transform.position.y || rb.transform.position.y > patrolStartPointY + 0.3f))
        {
            canMove = false;
            //rb.MovePosition(new Vector2(rb.transform.position.x, patrolStartPointY));
            StartCoroutine(Telp());
            
            //rb.transform.position = new Vector2(rb.transform.position.x, patrolStartPointY);
            Debug.Log("I am called!");
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
            canMove = true;
            currentAIState = AIState.CHASE;
        }
        else if (suspiciousThreshold < suspiciousValue && suspiciousValue < chaseThreshold)
        {
            canMove = false;
        }

        if (suspiciousValue == 0f)
        {
            canMove = true;
            ChangeAIState(AIState.RETURNTOPATROL);
        }
    }

    private void MoveLeft()
    {
        if(currentAIState != AIState.NONE)
        {
            if(currentAIState == AIState.CHASE)
            {
                transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * chaseMovementMultiplier * Time.deltaTime), rb.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * Time.deltaTime), rb.position.y);
            }
            spriteRenderer.flipX = true;
            visionBoxObject.transform.localPosition = new Vector3(defaultVisionBoxPos.x * -1, defaultVisionBoxPos.y);
            //visionBoxObject.transform.position = new Vector3(-defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
            //spriteRenderer.color = defaultColor;
        }
    }

    private void MoveRight()
    {
        if(currentAIState != AIState.NONE)
        {
            if(currentAIState == AIState.CHASE)
            {
                transform.position = new Vector2(transform.position.x + (normalMovementSpeed * chaseMovementMultiplier * Time.deltaTime), rb.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x + (normalMovementSpeed * Time.deltaTime), rb.position.y);
            }
            spriteRenderer.flipX = false;
            visionBoxObject.transform.localPosition = defaultVisionBoxPos;
            //visionBoxObject.transform.position = new Vector3(defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
            //spriteRenderer.color = Color.yellow;
        }
    }
    
    public void ChangeAIState(AIState newAIState)
    {
        currentAIState = newAIState;
    }

    public AIState GetLastAIState()
    {
        return currentAIState;
    }

    public float GetSuspiciousValue()
    {
        return suspiciousValue;
    }

    public void SetSuspiciousValue(float newSuspiciousValue)
    {
        suspiciousValue = newSuspiciousValue;
    }
}

public enum AIState
{
    NONE,
    PATROLTO,
    PATROLBACK,
    SUSPICIOUS,
    CHASE,
    RETURNTOPATROL
}


