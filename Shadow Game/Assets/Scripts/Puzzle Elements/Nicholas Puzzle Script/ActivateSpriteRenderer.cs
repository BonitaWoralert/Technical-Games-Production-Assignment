using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpriteRenderer : MonoBehaviour
{
    //This script is made to enable or disable the sprite renderer depending on the switch's state.
    [SerializeField] private BaseSwitch _active;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_active.GetState())
        {
            //When switch is active
            _spriteRenderer.enabled = true;
        }
        else
        {
            //When switch is not active
            _spriteRenderer.enabled = false;
        }
    }
}
