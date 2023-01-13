using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private BaseSwitch _active;
    [SerializeField] private GameObject _box;

    private void Update()
    {
        if (_active.GetState())
        {
            _box.transform.position = transform.position;
        }
    }
}
