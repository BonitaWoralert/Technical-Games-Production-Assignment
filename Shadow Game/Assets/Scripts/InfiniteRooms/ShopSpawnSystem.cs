using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawnSystem : MonoBehaviour
{
    public List<GameObject> upgradeLocations;
    public List<GameObject> upgrades;
    UpgradeList upgradeList;
    int upgradeValue;
    // Start is called before the first frame update
    void Start()
    {
        upgradeList = FindObjectOfType<UpgradeList>();
        foreach (var i in upgradeLocations)
        {
            Spawn(i);
        }
    }
    void Spawn(GameObject i)
    {
        upgradeValue = Random.Range(0, upgrades.Count);
        if (upgradeList.upgradeDetails[upgradeValue].uses > 0)
        {
            Instantiate(upgrades[upgradeValue], i.transform.position, Quaternion.identity);
        }
        else
        {
            Spawn(i);
        }
    }

    
}
