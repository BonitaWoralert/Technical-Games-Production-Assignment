using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    [SerializeField] private BaseSwitch _active1;
    [SerializeField] private BaseSwitch _active2;
    [SerializeField] private GameObject _light1;
    [SerializeField] private GameObject _light2;

    // Update is called once per frame
    private void Update()
    {
        if (_active1.GetState())
        {
            _light1.SetActive(true);
        }
        else
        {
            _light1.SetActive(false);
        }
        
        if (_active2.GetState())
        {
            _light2.SetActive(true);
        }
        else
        {
            _light2.SetActive(false);
        }
    }
}
