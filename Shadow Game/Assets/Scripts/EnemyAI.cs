using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// The script should make the AI to:
    ///     Patrol
    ///     Wonder (Suspicious)
    ///     Chase
    ///     Return (Go back to patrol pattern location)
    /// </summary>

    private AIState currentAIState;
    [SerializeField] private float patrolStartPointX;
    [SerializeField] private float patrolStartPointY;
    [SerializeField] private float patrolEndPointX;
    [SerializeField] private float patrolPointRange;
    [SerializeField] private float normalMovementSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool moveLeft;

    void Start()
    {
        currentAIState = AIState.PATROLTO;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = rb.position.y;

        if (patrolStartPointX < patrolEndPointX)
        {
            moveLeft = false;
        }
        else
        {
            moveLeft = true;
        }
    }

    void Update()
    {
        if(moveLeft == true)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }

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
                break;
            case AIState.CHASE:
                break;
            case AIState.RETURN:
                break;
            default:
                break;
        }
  
    }

    private void PatrolTo()
    {
        if(patrolEndPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolEndPointX + patrolPointRange)
        {
            //Enemy is in range of patrolEndPoint. Start going back.
            currentAIState = AIState.PATROLBACK;

            //Change Movement Direction
            moveLeft = !moveLeft;
        }
    }

    private void PatrolBack()
    {
        if(patrolStartPointX - patrolPointRange <= transform.position.x && transform.position.x <= patrolStartPointX + patrolPointRange)
        {
            //Enemy is in range of patrolStartPoint. Start going to.
            currentAIState = AIState.PATROLTO;

            //Change Movement Direction
            moveLeft = !moveLeft;
        }
    }

    private void MoveLeft()
    {
        transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * Time.deltaTime), rb.position.y);
    }

    private void MoveRight()
    {
        transform.position = new Vector2(transform.position.x + (normalMovementSpeed * Time.deltaTime), rb.position.y);
    }
}

enum AIState
{
    NONE,
    PATROLTO,
    PATROLBACK,
    SUSPICIOUS,
    CHASE,
    RETURN
}
