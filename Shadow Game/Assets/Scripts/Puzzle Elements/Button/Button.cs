using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : BaseSwitch
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _state = true;
        animator.SetBool("isActive", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _state = false;
        animator.SetBool("isActive", false);
    }
}
