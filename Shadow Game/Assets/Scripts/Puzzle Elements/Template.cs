using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
    [SerializeField] private BaseSwitch _active;

    private void Update()
    {
        if (_active.GetState())
        {
            //When switch is active
        }
        else
        {
            //When switch is not active
        }
    }
}
