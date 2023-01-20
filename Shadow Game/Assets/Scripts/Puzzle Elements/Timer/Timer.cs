using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : BaseSwitch
{
    [SerializeField] private BaseSwitch _active;

    [SerializeField] private float _maxTime;
    private float _time;

    private void Update()
    {
        if (_active.GetState())
        {
            _time = _maxTime;
        }

        if (_time > 0)
        {
            _state = true;
            _time -= Time.deltaTime;
        }
        else
        {
            _state = false;
        }
    }
}
