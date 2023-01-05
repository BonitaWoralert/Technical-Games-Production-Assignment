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

    /// <summary>
    /// Sets how much light is hitting the object
    /// </summary>
    /// <param name="intensity">How much light is hitting the object</param>
    public void SetIntenisty(float intensity)
    {
        _intenisty = intensity;
    }

    public void AddIntenisty(float intensity)
    {
        _intenisty += intensity;
    }
}
