using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivator : BaseSwitch
{
    private LightDetectorReceiver _lightReciver;
    private Animator animator;

    [SerializeField] private float _requiredLight;

    private void Awake()
    {
        _lightReciver = GetComponent<LightDetectorReceiver>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_lightReciver._intenisty > _requiredLight)
        {
            _state = true;
            animator.SetBool("isActive", true);
        }
        else
        {
            _state = false;
            animator.SetBool("isActive", false);
        }
    }
}
