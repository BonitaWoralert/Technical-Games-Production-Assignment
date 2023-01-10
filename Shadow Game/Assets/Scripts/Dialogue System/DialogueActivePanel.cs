using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private BaseSwitch _active;

    private void Update()
    {
        if (_active.GetState())
        {
            _panel.SetActive(true);
        }
    }
}
