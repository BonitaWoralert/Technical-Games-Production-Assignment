using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicOR : BaseSwitch
{
    [SerializeField] private BaseSwitch _input1;
    [SerializeField] private BaseSwitch _input2;

    private void Update()
    {
        _state = _input1.GetState() && _input2.GetState();
    }
}
