using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LightDetectorReciver : MonoBehaviour
{
    private float _intenisty = 0f;

    private void Update()
    {
        print(_intenisty);
    }

    public void SetIntenisty(float intensity)
    {
        _intenisty = intensity;
    }
}
