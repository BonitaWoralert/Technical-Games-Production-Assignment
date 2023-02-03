using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicSWITCH : BaseSwitch
{
    [SerializeField] private BaseSwitch _active;

    void Update()
    {
        if (_active.GetState())
        {
            _state = !_state;
        }
    }
}
