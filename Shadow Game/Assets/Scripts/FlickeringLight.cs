using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    private Light2D _light;
    private float _baseIntensity;

    private int _count;
    [SerializeField][Range(0f, 0.1f)][Tooltip("Speed on the sine graph, between 0 and 360.")] private float _speed = 0.05f;

    [SerializeField] float _flicker;

    private void Awake()
    {
        _light = GetComponent<Light2D>();

        _baseIntensity = _light.intensity;
    }

    private void Update()
    {
        _count++;
        _light.intensity = _baseIntensity + (Mathf.Sin(_count * _speed) * _flicker);
        //_light.intensity = Random.Range(_baseIntensity - _flicker, _baseIntensity + _flicker);
    }
}
