using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private List<GameObject> upgrades;
    public bool isShopEmpty;
    UpgradeList upgradeList;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        upgradeList = FindObjectOfType<UpgradeList>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var i in GameObject.FindGameObjectsWithTag("Upgrade"))
        {
            if (Vector2.Distance(player.transform.position, i.transform.position) <= 2f && Input.GetButtonDown("Interact"))
            {
                if (i.GetComponent<Upgrades>())
                {
                    Upgrades currentUpgrade = i.GetComponent<Upgrades>();
                    if (currentUpgrade.upgradeType == Upgrades.UpgradeType.MoreCoinsPerRoom)
                    {
                        player.GetComponent<PlayerStats>().roomCoins += 2;
                    }
                    if (currentUpgrade.upgradeType == Upgrades.UpgradeType.DashPower)
                    {
                        player.GetComponent<Movement>().dashSpace += 3;
                    }
                }
            }
        }

        isShopEmpty = true;
        foreach (var i in upgradeList.upgradeDetails)
        {
            if (i.uses > 0)
            {
                isShopEmpty = false;
            }
        }
    }
}
