using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFlip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < playerTransform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
