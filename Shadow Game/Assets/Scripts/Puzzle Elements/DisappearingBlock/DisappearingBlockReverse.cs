using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlockReverse : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    [SerializeField] private BaseSwitch _active;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _boxCollider.enabled = !_active.GetState();

        Color TempColor = _spriteRenderer.color;
        if (!_active.GetState())
        {
            TempColor.a = 1f;
        }
        else
        {
            TempColor.a = 0.4f;
        }
        _spriteRenderer.color = TempColor;
    }
}
