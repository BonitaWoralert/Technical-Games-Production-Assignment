using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Emits the raycasts for object detection
/// </summary>
public class LightDetectorEmitter : MonoBehaviour
{
    List<GameObject> _exposedObjects = new List<GameObject>();

    Light2D _attachedLight;

    private void Awake()
    {
        _attachedLight = GetComponent<Light2D>();
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _exposedObjects.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (_exposedObjects[i].transform.position - transform.position).normalized);
            if (hit.collider.name == _exposedObjects[i].name)
            {
                //Calculates the intenisty of the light hitting the object, taking the object's position into account
                float spacialIntenisty = (1 - (hit.distance / (_attachedLight.pointLightOuterRadius - _attachedLight.pointLightInnerRadius))) * _attachedLight.intensity;
                _exposedObjects[i].GetComponent<LightDetectorReceiver>().AddIntenisty(spacialIntenisty);
            }
            else
            {
                //Resets intesnisty to 0 when they are in shadow
                _exposedObjects[i].GetComponent<LightDetectorReceiver>().SetIntenisty(0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger Enter");
        if (collision.GetComponent<LightDetectorReceiver>() != null)
        {
            _exposedObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Trigger Exit");
        if (collision.GetComponent<LightDetectorReceiver>() != null)
        {
            collision.GetComponent<LightDetectorReceiver>().SetIntenisty(0f);//Resets to 0 when leaving the light area
            _exposedObjects.Remove(collision.gameObject);
        }
    }
}
