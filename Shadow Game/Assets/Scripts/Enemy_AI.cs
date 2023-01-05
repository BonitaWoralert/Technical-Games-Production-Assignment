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

    [SerializeField] private float patrolStartPointX;
    [SerializeField] private float patrolStartPointY;
    [SerializeField] private float patrolEndPointX;
    [SerializeField] private float patrolPointRange;
    [SerializeField] private float normalMovementSpeed;
    [SerializeField] private bool isMoveLeft;
    [SerializeField] public bool canMove;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject visionBoxObject;

    [SerializeField] private float suspiciousValue;
    [SerializeField] private float suspiciousFillUpSpeed;
    [SerializeField] private float suspiciousDrainSpeed;

    [SerializeField] private AIState currentAIState;
    [SerializeField] private GameObject playerObject;
    //[SerializeField] private Enemy_Vision_Script enemyVisionScript;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        currentAIState = AIState.PATROLTO;
        defaultVisionBoxPos = visionBoxObject.transform.localPosition;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = rb.position.y;
        playerObject = GameObject.Find("Player");
        canMove = true;
        isPlayerSpotted = false;

        suspiciousValue = 0f;
        suspiciousFillUpSpeed = 30f;
        suspiciousDrainSpeed = 15f;

        if (patrolStartPointX < patrolEndPointX)
        {
            isMoveLeft = false;
        }
        else
        {
            isMoveLeft = true;
        }
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

        //if (currentAIState == AIState.SUSPICIOUS || currentAIState == AIState.CHASE)
        if(isPlayerSpotted)
        {
            //canMove = false;
            if(suspiciousValue > 120f)
            {
                suspiciousValue = 120f;
            }
            else
            {
                suspiciousValue += suspiciousFillUpSpeed * Time.deltaTime;
            }

        }
        else
        {
            //canMove = true;
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

        if(100f > suspiciousValue && suspiciousValue > 50f)
        {
            ChangeAIState(AIState.SUSPICIOUS);
        }
    }

    private void PatrolTo()
    {
        if(patrolEndPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolEndPointX + patrolPointRange)
        {
            //Enemy is in range of patrolEndPoint. Start going back.
            currentAIState = AIState.PATROLBACK;

            //Change Movement Direction
            isMoveLeft = !isMoveLeft;
        }
    }

    private void PatrolBack()
    {
        if(patrolStartPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolStartPointX + patrolPointRange)
        {
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState = AIState.PATROLTO;

            //Change Movement Direction
            isMoveLeft = !isMoveLeft;
        }
    }

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
        canMove = true;
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
    }

    private void Suspicious()
    {
        //AI does not move, until suspicious bar fills up.
        if (suspiciousValue > 100)
        {
            canMove = true;
            currentAIState = AIState.CHASE;
        }
        else if (50 < suspiciousValue && suspiciousValue < 100)
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
            transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * Time.deltaTime), rb.position.y);
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
            transform.position = new Vector2(transform.position.x + (normalMovementSpeed * Time.deltaTime), rb.position.y);
            spriteRenderer.flipX = false;
            visionBoxObject.transform.localPosition = defaultVisionBoxPos;
            //visionBoxObject.transform.position = new Vector3(defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
            //spriteRenderer.color = Color.yellow;
        }
    }
    
    public void ChangeAIState(AIState newAIState)
    {
        //switch (newAIState)
        //{
        //    case 0:
        //        currentAIState = AIState.NONE;
        //        break;
        //
        //    case 1:
        //        currentAIState = AIState.PATROLTO;
        //        break;
        //
        //    case 2:
        //        currentAIState = AIState.PATROLBACK;
        //        break;
        //
        //    default:
        //        currentAIState = AIState.NONE;
        //        break;
        //}
        currentAIState = newAIState;
        Debug.Log("New AIState is: " + currentAIState);
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


