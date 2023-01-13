using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : BaseSwitch
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Ground")
        {
            _state = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Ground")
        {
            _state = false;
        }
    }
}
