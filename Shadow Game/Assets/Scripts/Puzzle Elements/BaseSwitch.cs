using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSwitch : MonoBehaviour
{
    protected bool _state = false;

    public bool GetState()
    {
        return _state;
    }
}