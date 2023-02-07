using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfiniteUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] List<GameObject> upgrades;
    [SerializeField] TextMeshProUGUI upgradeUIText;
    [SerializeField] List<GameObject> UI;
    private bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FindUpgrades();
        DetectIntersection();
        UICheck();
    }

    private void UICheck()
    {
        foreach (var i in UI)
        {
            i.SetActive(isInRange);
        }
    }

    private void DetectIntersection()
    {
        foreach (var i in upgrades)
        {
            if (Vector2.Distance(player.transform.position, i.transform.position) <= 2f)
            {
                isInRange = true;
                upgradeUIText.text = "Name: " + i.GetComponent<Upgrades>().name.ToString() + "\nCost: " + i.GetComponent<Upgrades>().cost.ToString();
                return;
            }
        }
            isInRange = false;
    }

    private void FindUpgrades()
    {
        upgrades.Clear();
        foreach (var i in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            upgrades.Add(i);
        }
    }
}
