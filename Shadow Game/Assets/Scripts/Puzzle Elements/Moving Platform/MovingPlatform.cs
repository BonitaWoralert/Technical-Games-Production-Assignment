using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private BaseSwitch _active;
    [SerializeField] private Transform _goalPosition;
    private Vector2 _startPosition;
    private bool _toGoal = true;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_active.GetState())
        {
            if (_toGoal)
            {
                transform.position = Vector2.MoveTowards(transform.position, _goalPosition.position, Time.deltaTime);
                if (transform.position == _goalPosition.position)
                {
                    print("Arrived at goal");
                    _toGoal = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, _startPosition, Time.deltaTime);
                if (new Vector2(transform.position.x, transform.position.y) == _startPosition)
                {
                    print("Arrived at Start");
                    _toGoal = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
