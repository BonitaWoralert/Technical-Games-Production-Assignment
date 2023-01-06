using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool _state = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        _state = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _state = false;
    }

    public bool GetState()
    {
        return _state;
    }
}
