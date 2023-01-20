using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    private Light2D _light;
    private float _baseIntensity;
    private float _baseOuterRadius;
    private float _baseInnerRadius;


    private int _count;
    [SerializeField][Range(0f, 3f)] private float _speed = 1f;

    [SerializeField] float _flicker;

    private void Awake()
    {
        _light = GetComponent<Light2D>();

        _baseIntensity = _light.intensity;
        _baseOuterRadius = _light.pointLightOuterRadius;
        _baseInnerRadius = _light.pointLightInnerRadius;
    }

    private void Update()
    {
        _count++;
        float baseValue = Mathf.Sin(_count * _speed/100) * _flicker;
        _light.intensity = _baseIntensity + baseValue;
        _light.pointLightOuterRadius = _baseOuterRadius + baseValue/2;
        _light.pointLightInnerRadius = _baseInnerRadius + baseValue/2;
        //_light.intensity = Random.Range(_baseIntensity - _flicker, _baseIntensity + _flicker);
    }
}
