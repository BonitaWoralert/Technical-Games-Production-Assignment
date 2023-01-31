using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : BaseSwitch
{
    private bool _playerInRange;

    private void Update()
    {
        if ((_playerInRange && Input.GetButtonDown("Interact")) || _state)
        {
            _state = !_state;
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
