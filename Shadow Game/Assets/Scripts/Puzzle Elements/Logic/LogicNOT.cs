using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicNOT : BaseSwitch
{
    [SerializeField] private BaseSwitch _input;

    private void Update()
    {
        _state = !(_input.GetState());
    }
}
