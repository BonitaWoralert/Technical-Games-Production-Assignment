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

    private Sprite playerSprite;
    private PlayerStats stats;
    private Animator animator;
    private Movement movement;
    private LightDetectorReceiver lightDetectorReceiver;

    // Start is called before the first frame update
    void Start()
    {
        isInShadowForm = false;
        playerSprite = GetComponent<Sprite>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        lightDetectorReceiver = GetComponent<LightDetectorReceiver>();
    }

    // Update is called once per frame
    void Update()
    {
        //Logic for if Player is in Darkness

        Change();
    }
    private void LateUpdate()
    {
        intensity = lightDetectorReceiver._intenisty;
    }
    void Change()
    {
        shadowLight.enabled = isInShadowForm;

        if (Input.GetKeyDown(KeyCode.F) && isInDarkness && movement.GetGrounded())
        {
            if (isInShadowForm)
            {
                isInShadowForm = !isInShadowForm;
                transform.gameObject.layer = 7;
            }
            else if (!isInShadowForm && stats.shadowEnergy > 0f && intensity <= 0)
            {
                isInShadowForm = !isInShadowForm;
                transform.gameObject.layer = 9;
            }
        }

        if (stats.shadowEnergy <= 0)
        {
            isInShadowForm = false;
            transform.gameObject.layer = 7;
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
}
