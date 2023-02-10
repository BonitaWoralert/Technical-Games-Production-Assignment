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
        if (collision.tag == "Player" || collision.tag == "PuzzleBox")
        {
            animator.SetBool("isActive", true);
            _state = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "PuzzleBox")
        {
            _state = false;
            animator.SetBool("isActive", false);
        }
    }
}
