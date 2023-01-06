using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowForm : MonoBehaviour
{
    public bool isInShadowForm;
    public bool isInDarkness;
    public Light2D shadowLight;

    private Sprite playerSprite;
    private PlayerStats stats;
    private Animator animator;
    private Movement movement;
    // Start is called before the first frame update
    void Start()
    {
        isInShadowForm = false;
        playerSprite = GetComponent<Sprite>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Logic for if Player is in Darkness

        Change();
    }

    void Change()
    {
        shadowLight.enabled = isInShadowForm;

        if (Input.GetKeyDown(KeyCode.F) && isInDarkness && movement.isGrounded)
        {
            if (isInShadowForm)
            {
                isInShadowForm = !isInShadowForm;
            }
            else if (!isInShadowForm && stats.shadowEnergy > 0f)
            {
                isInShadowForm = !isInShadowForm;
            }
        }

        if (stats.shadowEnergy <= 0)
        {
            isInShadowForm = false;
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
