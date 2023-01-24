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
    private ShadowMovement shadowMovement;

    // Start is called before the first frame update
    void Start()
    {
        isInShadowForm = false;
        playerSprite = GetComponent<Sprite>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        lightDetectorReceiver = GetComponent<LightDetectorReceiver>();
        shadowMovement = GetComponent<ShadowMovement>();
        playerBoxCollider.enabled = true;
        shadowBoxCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        Change();
    }
    private void LateUpdate()
    {
        intensity = lightDetectorReceiver._intenisty;
    }
    void Change()
    {
        shadowLight.enabled = isInShadowForm;

        if (Input.GetKeyDown(KeyCode.F) && isInDarkness)
        {
            if (isInShadowForm)
            {
                PlayerFormChange();
            }
            else if (!isInShadowForm && stats.shadowEnergy > 0f && intensity <= 0 && movement.isGrounded)
            {
                ShadowFormChange();
            }
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

    public void PlayerFormChange()
    {
        isInShadowForm = !isInShadowForm;
        playerBoxCollider.enabled = true;
        shadowBoxCollider.enabled = false;
        transform.gameObject.layer = 7;
        rb.gravityScale = 1.7f;
        shadowMovement.Refresh();
    }
    public void ShadowFormChange()
    {
        isInShadowForm = !isInShadowForm;
        playerBoxCollider.enabled = false;
        shadowBoxCollider.enabled = true;
        transform.gameObject.layer = 9;
        rb.gravityScale = 0;
    }
}
