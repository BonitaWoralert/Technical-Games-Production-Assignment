using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Acts as a receiver to the light emiter, and stores the spacial intestisty of the lights
/// </summary>
public class LightDetectorReceiver : MonoBehaviour
{
    public float _intenisty = 0f;

    public bool _update;

    private void Update()
    {
        if (_update)
        {
            _intenisty = 0f;
        }
        _update = !_update;
    }

    public void AddIntenisty(float intensity)
    {
        _intenisty += intensity;
    }
}
