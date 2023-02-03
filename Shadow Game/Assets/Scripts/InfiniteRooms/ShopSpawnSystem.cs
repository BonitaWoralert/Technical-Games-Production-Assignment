using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawnSystem : MonoBehaviour
{
    public List<GameObject> upgradeLocations;
    public List<GameObject> upgrades;
    public UpgradeList upgradeList;
    public UpgradeDetails upgradeDetails;
    public Shop shop;
    public int upgradeValue;
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        shop = FindObjectOfType<Shop>();
        upgradeList = FindObjectOfType<UpgradeList>();
        upgrades = FindObjectOfType<Shop>().upgrades;
        foreach (var i in upgradeLocations)
        {
            Debug.Log("Spawning Upgrades");
            Spawn(i);
        }
    }
    void Spawn(GameObject i)
    {
        if (shop.isShopEmpty)
        {
            return;
        }
        upgradeValue = Random.Range(0, upgradeList.upgradeDetails.Count);
        Debug.Log("Finding Value" + upgradeValue.ToString());
        if (upgradeList.upgradeDetails[upgradeValue].uses > 0)
        {
            Debug.Log("Instantiating Upgrade");
            GameObject upgrade = Instantiate(upgradeList.upgradeDetails[upgradeValue].prefab, i.transform.position, Quaternion.identity);
            upgrade.GetComponent<Upgrades>().id = upgradeValue;
        }
        else
        {
            Debug.Log("Couldnt Find Upgrade");
            Spawn(i);
        }
    }
}
