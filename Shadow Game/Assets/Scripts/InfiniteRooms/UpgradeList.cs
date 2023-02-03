using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    public List<UpgradeDetails> upgradeDetails;
    Shop shopSpawn;
    void Start()
    {
        shopSpawn = FindObjectOfType<Shop>();
    }

    void Update()
    {
        if (shopSpawn.isShopEmpty)
        {
            Debug.Log("Shop Empty");
        }
    }
}
[System.Serializable]

public class UpgradeDetails
{
    public string name;
    public int uses;
    public GameObject prefab;
}
