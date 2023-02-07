using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private InfiniteStats stats;
    private bool itemCollected = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DashItem")
        {
            itemCollected = true;
            if (itemCollected)
            {
                movement.DashAdd(1);
                itemCollected = false;
            }
        }
        if (collision.gameObject.name == "CoinItem")
        {
            itemCollected = true;
            if (itemCollected)
            {
                stats.coins += 1;
                itemCollected = false;
            }
        }
    }
}
