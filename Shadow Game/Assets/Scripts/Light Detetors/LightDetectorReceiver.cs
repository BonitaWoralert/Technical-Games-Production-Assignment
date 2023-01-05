using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// Acts as a receiver to the light emiter, and stores the spacial intestisty of the lights
/// </summary>
public class LightDetectorReceiver : MonoBehaviour
{
    private float _intenisty = 0f;

    public void SetIntenisty(float intensity)
    {
        print(_intenisty);
        _intenisty = intensity;
    }

    public void AddIntenisty(float intensity)
    {
        _intenisty += intensity;
    }
}
