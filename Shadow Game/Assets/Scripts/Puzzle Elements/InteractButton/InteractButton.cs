using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : BaseSwitch
{
    private bool _playerInRange;
    private SpriteRenderer _spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if ((_playerInRange && Input.GetKeyDown("e")) || _state)
        {
            _state = !_state;
        }

        StateSwitch();
    }

    private void StateSwitch()
    {
        if (_state)
        {
            animator.SetBool("isActive", true);
        }
        else
        {
            animator.SetBool("isActive", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerInRange = false;
        }
    }
}
