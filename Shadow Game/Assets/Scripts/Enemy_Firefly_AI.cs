using System.Collections;
using UnityEngine;

public class Enemy_Firefly_AI : MonoBehaviour
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

    [Space(20)]

    [SerializeField] private float moveWaveFrequency;
    [SerializeField] private float moveWaveMagnitude;
    [SerializeField] private float moveWaveOffset;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 localScale;

    public bool canMove;

    private Movement move;
    private ShadowMovement shadowMove;
    private Animator animator;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.GetComponent<Rigidbody2D>();
        defaultColor = spriteRenderer.color;
        patrolStartPointX = rb.position.x;
        patrolStartPointY = transform.position.y;
        patrolEndPointX = patrolStartPointX + patrolEndPointOffsetX;
        resetPosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        playerObject = GameObject.Find("Player");
        canMove = true;
        isPlayerSpotted = false;
        destination = new Vector3(patrolEndPointX, patrolStartPointY, 0);
        startPosition = transform.position;
        position = startPosition;

        timeToMove = defaultTimeToMove;
        move = playerObject.GetComponent<Movement>();
        shadowMove = playerObject.GetComponent<ShadowMovement>();



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

        //MoveTimer();
        //SlimeAnimationCheck();
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
        if(velocityY > 0)
        {
            
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
            rb.velocity += new Vector2(-movementForceX, 0f);
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
        if (Vector2.Distance(gameObject.transform.position, shadowMove.leftCheck.transform.position) < Vector2.Distance(gameObject.transform.position, shadowMove.rightCheck.transform.position))
        {
            ChangeDestination(shadowMove.leftCheck.transform.position - playerDistanceBuffer);
        }
        else
        {
            ChangeDestination(shadowMove.rightCheck.transform.position + playerDistanceBuffer);
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
            position -= transform.right * Time.deltaTime * normalMovementSpeed;
            transform.position = position + transform.up * Mathf.Sin(Time.time * moveWaveFrequency + moveWaveOffset) * moveWaveMagnitude;
            velocityX = rb.velocity.x;
            velocityY = rb.velocity.y;
            spriteRenderer.flipX = false;
        }
    }

    private void MoveRight()
    {
        if(currentAIState2 != AIState2.NONE)
        {
            position += transform.right * Time.deltaTime * normalMovementSpeed;
            transform.position = position + transform.up * Mathf.Sin(Time.time * moveWaveFrequency + moveWaveOffset) * moveWaveMagnitude;
            velocityX = rb.velocity.x;
            velocityY = rb.velocity.y;
            spriteRenderer.flipX = true;
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
        Gizmos.DrawLine(gameObject.transform.position, startPosition * Mathf.Sin(Time.time * moveWaveFrequency + moveWaveOffset) * moveWaveMagnitude);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}



