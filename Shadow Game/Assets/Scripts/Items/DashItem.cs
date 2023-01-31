using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    private bool itemCollected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DashItem")
        {
            itemCollected = true;
            if (itemCollected)
            {
                stats.dashAmount++;
                itemCollected = false;
            }
        }
    }
}
