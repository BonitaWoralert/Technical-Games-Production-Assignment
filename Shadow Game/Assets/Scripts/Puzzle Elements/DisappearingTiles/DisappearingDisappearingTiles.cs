using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearingTiles : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private TilemapCollider2D _collider;

    [SerializeField] private BaseSwitch _active;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<TilemapCollider2D>();
    }

    private void Update()
    {
        _collider.enabled = _active.GetState();

        Color TempColor = _spriteRenderer.color;
        if (_active.GetState())
        {
            TempColor.a = 1f;
        }
        else
        {
            TempColor.a = 0.6f;
        }
        _spriteRenderer.color = TempColor;
    }
}
