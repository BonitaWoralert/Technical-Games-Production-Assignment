using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleLight : MonoBehaviour
{
    private Light2D _light;
    [SerializeField] private Button _active;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {
        _light.enabled = _active.GetState();
    }
}
