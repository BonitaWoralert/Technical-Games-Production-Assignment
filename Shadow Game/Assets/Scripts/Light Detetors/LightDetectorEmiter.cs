using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Emits the raycasts for object detection
/// </summary>
public class LightDetectorEmiter : MonoBehaviour
{
    List<GameObject> _exposedObjects = new List<GameObject>();

    Light2D _attachedLight;

    private void Awake()
    {
        _attachedLight = GetComponent<Light2D>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < _exposedObjects.Count; i++)
        {
            Debug.DrawRay(transform.position, (_exposedObjects[i].transform.position - transform.position).normalized, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (_exposedObjects[i].transform.position - transform.position).normalized);
            if (hit.collider.name == _exposedObjects[i].name)
            {
                float spacialIntenisty = (1 - (hit.distance / (_attachedLight.pointLightOuterRadius - _attachedLight.pointLightInnerRadius))) * _attachedLight.intensity;
                //print(hit.distance);
                _exposedObjects[i].GetComponent<LightDetectorReciver>().SetIntenisty(spacialIntenisty);
            }
            else
            {
                _exposedObjects[i].GetComponent<LightDetectorReciver>().SetIntenisty(0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger Enter");
        if (collision.GetComponent<LightDetectorReciver>() != null)
        {
            _exposedObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Trigger Exit");
        if (collision.GetComponent<LightDetectorReciver>() != null)
        {
            collision.GetComponent<LightDetectorReciver>().SetIntenisty(0f);
            _exposedObjects.Remove(collision.gameObject);
        }
    }
}
