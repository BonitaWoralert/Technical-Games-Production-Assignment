using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Emits the raycasts for object detection
/// </summary>
public class LightDetectorEmiter : MonoBehaviour
{
    List<GameObject> _exposedObjects = new List<GameObject>();

    private void FixedUpdate()
    {
        for (int i = 0; i < _exposedObjects.Count; i++)
        {
            Debug.DrawRay(transform.position, (_exposedObjects[i].transform.position - transform.position).normalized, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (_exposedObjects[i].transform.position - transform.position).normalized);
            if (hit.collider.name == _exposedObjects[i].name)
            {
                print("In Light");
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
            _exposedObjects.Remove(collision.gameObject);
        }
    }
}
