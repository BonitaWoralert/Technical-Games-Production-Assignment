using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicAND : BaseSwitch
{
    [SerializeField] private BaseSwitch _input1;
    [SerializeField] private BaseSwitch _input2;

    private void Update()
    {
        if (_input1.GetState() && _input2.GetState())
        {
            _state = true;
        }
        else
        {
            _state = false;
        }
    }
}
