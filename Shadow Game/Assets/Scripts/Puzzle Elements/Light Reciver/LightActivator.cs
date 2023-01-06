using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivator : MonoBehaviour
{
    private LightDetectorReceiver _lightReciver;

    private bool _state;

    [SerializeField] private float _requiredLight;

    private void Awake()
    {
        _lightReciver = GetComponent<LightDetectorReceiver>();
    }

    private void Update()
    {
        if (_lightReciver._intenisty > _requiredLight)
        {
            _state = true;
        }
        else
        {
            _state = false;
        }
    }

    private bool GetState()
    {
        return _state;
    }
}
