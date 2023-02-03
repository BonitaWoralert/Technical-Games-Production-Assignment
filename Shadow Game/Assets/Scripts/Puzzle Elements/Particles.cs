using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Particles : MonoBehaviour
{
    [SerializeField] private GameObject endPos;
    [SerializeField] private BaseSwitch _active;
    private VisualEffect effect;

    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
    }

    private void Start()
    {
        effect.SetVector3("_target", endPos.transform.localPosition);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Line"))
        {
            effect.SetBool("_active", !effect.GetBool("_active"));
        }
        effect.SetBool("_switch", _active.GetState());
    }
}
