using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge : MonoBehaviour
{
    [SerializeField] private BaseSwitch _active;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(_active.GetState())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
