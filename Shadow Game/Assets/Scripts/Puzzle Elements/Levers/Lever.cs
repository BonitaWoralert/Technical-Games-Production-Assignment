using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : BaseSwitch
{
    private bool _playerInRange;
    private Color _activeColor;
    private Color _inactiveColor;
    private SpriteRenderer _spriteRenderer;
    private Animator animator;
    [SerializeField] private AudioSource switchSound;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _activeColor = Color.green;
        _inactiveColor = Color.red;
    }

    private void Update()
    {
        if (_playerInRange && Input.GetButtonDown("Interact"))
        {
            switchSound.Play();
            _state = !_state;
        }

        ColorSwitch();
    }

    private void ColorSwitch()
    {
        if (_state)
        {
            animator.SetBool("isActive", true);
            _spriteRenderer.color = _activeColor;
        }
        else
        {
            animator.SetBool("isActive", false);
            _spriteRenderer.color = _inactiveColor;
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
