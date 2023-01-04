using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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

    private AIState currentAIState;
    private bool isSpriteFlip;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private Vector2 defaultVisionBoxPos;

    [SerializeField] private float patrolStartPointX;
    [SerializeField] private float patrolStartPointY;
    [SerializeField] private float patrolEndPointX;
    [SerializeField] private float patrolPointRange;
    [SerializeField] private float normalMovementSpeed;
    [SerializeField] private bool isMoveLeft;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject visionBoxObject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        currentAIState = AIState.PATROLTO;
        defaultVisionBoxPos = visionBoxObject.transform.localPosition;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = rb.position.y;

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
        if(isMoveLeft == true)
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

    private void MoveLeft()
    {
        transform.position = new Vector2(transform.position.x + -(normalMovementSpeed * Time.deltaTime), rb.position.y);
        spriteRenderer.flipX = true;
        visionBoxObject.transform.localPosition = new Vector3(defaultVisionBoxPos.x * -1, defaultVisionBoxPos.y);
        //visionBoxObject.transform.position = new Vector3(-defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
        //spriteRenderer.color = defaultColor;
    }

    private void MoveRight()
    {
        transform.position = new Vector2(transform.position.x + (normalMovementSpeed * Time.deltaTime), rb.position.y);
        spriteRenderer.flipX = false;
        visionBoxObject.transform.localPosition = defaultVisionBoxPos;
        //visionBoxObject.transform.position = new Vector3(defaultVisionBoxPosX, visionBoxObject.transform.position.y, visionBoxObject.transform.position.z);
        //spriteRenderer.color = Color.yellow;
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
