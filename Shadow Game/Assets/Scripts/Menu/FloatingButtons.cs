using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingButtons : MonoBehaviour
{
    [SerializeField] private float xPos = 0f;
    [SerializeField] private float amplitude = 1.7f;
    [SerializeField] private float frequency = 0.7f;

    Vector2 posOffset = new Vector2();
    Vector2 tempPos = new Vector2();
    public bool canDance;

    void Start()
    {
        posOffset = transform.position;

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
    public void OnPointerDownDelegate(PointerEventData data)
    {
        Debug.Log("OnPointerDownDelegate called.");
    }

    private void Update()
    {
        if (canDance == true)
        {
            tempPos = posOffset;
            tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * xPos;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
        else if (canDance == false)
        {
            tempPos.x = posOffset.x;
            tempPos.y = posOffset.y;

            transform.position = tempPos;
        }
    }

    public void SetDance(bool dancey)
    {
        canDance = dancey;
    }
}
