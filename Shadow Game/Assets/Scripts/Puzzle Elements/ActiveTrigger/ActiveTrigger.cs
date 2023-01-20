using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrigger : BaseSwitch
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _state = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _state = false;
    }
}
