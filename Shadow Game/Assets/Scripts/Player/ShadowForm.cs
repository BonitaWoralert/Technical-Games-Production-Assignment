using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowForm : MonoBehaviour
{
    public bool isInShadowForm;
    public bool isInDarkness;
    public Light2D shadowLight;
    public float intensity;

    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private BoxCollider2D shadowBoxCollider;
    private Sprite playerSprite;
    private PlayerStats stats;
    private Animator animator;
    private Movement movement;
    private LightDetectorReceiver lightDetectorReceiver;
    private Rigidbody2D rb;
    private PlayerStats playerStats;

    private SpaceCheck spaceCheck;
    private Collider2D[] _spaceCheck;
    [SerializeField] private ContactFilter2D _spaceFilter;

    private Player_Box_Carry _boxCarry;

    private ShadowMovement ShadowMovement;

    // Start is called before the first frame update
    void Start()
    {
        isInShadowForm = false;
        playerSprite = GetComponent<Sprite>();
        playerStats = GetComponent<PlayerStats>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        lightDetectorReceiver = GetComponent<LightDetectorReceiver>();
        ShadowMovement = GetComponent<ShadowMovement>();
        rb = GetComponent<Rigidbody2D>();
        playerBoxCollider.enabled = true;
        shadowBoxCollider.enabled = false;
        spaceCheck = GetComponentInChildren<SpaceCheck>();
        _boxCarry = GetComponentInChildren<Player_Box_Carry>();
    }
    // Update is called once per frame
    void Update()
    {
        //Logic for if Player is in Darkness

        Change();

        if (isInShadowForm && rb.gravityScale != 0)
        {
            rb.gravityScale = 0;
        }

        
    }
    private void LateUpdate()
    {
        intensity = lightDetectorReceiver._intenisty;
    }
    void Change()
    {
        shadowLight.enabled = isInShadowForm;

        if (Input.GetButtonDown("Shadow") && isInDarkness && movement.GetGrounded() && !_boxCarry.GetBoxCarry())
        {
            if (isInShadowForm)
            {
                PlayerFormChange();
            }
            else if (!isInShadowForm && stats.shadowEnergy > 0f && intensity <= 0)
            {
                ShadowFormChange();
            }
        }

        if (intensity > 0.05f && isInShadowForm)
        {
            PlayerFormChange();
        }

        if (stats.shadowEnergy <= 0 && isInShadowForm)
        {
            PlayerFormChange();
        }

        //IS in Shadow Form
        if (isInShadowForm)
        {
            animator.SetBool("isPlayer", false);
        }

        //ISNT in Shadow Form
        if (!isInShadowForm)
        {
            animator.SetBool("isPlayer", true);
        }
    }

    void PlayerFormChange()
    {
        if (spaceCheck.GetSpace())
        {
            playerBoxCollider.enabled = true;
            isInShadowForm = !isInShadowForm;
            shadowBoxCollider.enabled = false;
            transform.gameObject.layer = 7;
            ShadowMovement.Refresh();
            rb.gravityScale = 1.7f;
            if (stats.shadowEnergy < 0f)
            {
                stats.shadowEnergy = 0f;
            }
        }
    }
    void ShadowFormChange()
    {
        isInShadowForm = !isInShadowForm;
        playerBoxCollider.enabled = false;
        shadowBoxCollider.enabled = true;
        transform.gameObject.layer = 9;
        rb.gravityScale = 0f;
    }
}
