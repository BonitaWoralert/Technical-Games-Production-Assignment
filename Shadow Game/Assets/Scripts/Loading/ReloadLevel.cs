using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    [SerializeField] private GameObject nextTrigger;
    [SerializeField] private GameObject reloadTrigger;
    [SerializeField] private GameObject[] lightActive;
    [SerializeField] private GameObject[] lightDeactive;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Lever lever;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.name == "Reload Level Trigger")
        {
            player.position = new Vector2(startPoint.position.x,startPoint.position.y);

            if (lever.GetState())
            {
                reloadTrigger.SetActive(false);
                nextTrigger.SetActive(true);

                foreach (GameObject light in lightActive)
                {
                    light.SetActive(true);
                }

                foreach (GameObject light in lightDeactive)
                {
                    light.SetActive(false);
                }
            }
        }
    }
}
