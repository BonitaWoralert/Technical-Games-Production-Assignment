using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerReversed : BaseSwitch
{
    [SerializeField] private BaseSwitch _active;

    [SerializeField] private float _maxTime;
    private float _time;
    private bool _enabled;

    private void Update()
    {
        if (_active.GetState())
        {
            _time = _maxTime;
            _enabled = true;
        }

        if (_enabled && _time > 0)
        {
            _time -= Time.deltaTime;
        }

        if (_time >= 0)
        {
            _state = false;
        }
        else
        {
            _state = true;
        }
    }
}
