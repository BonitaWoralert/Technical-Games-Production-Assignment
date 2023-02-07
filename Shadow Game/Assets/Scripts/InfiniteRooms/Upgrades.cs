using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public int id;
    public int cost;
    public string name;
    public ShopSpawnSystem shopSpawnSystem;
    public enum UpgradeType
    {
        Health,
        ShadowTimerIncrease,
        ShadowTimerDecrease,
        DashPower,
        DashTimer,
        MaxDashes,
        FasterShadowMovement,
        MoreCoinsPerRoom,
        FurryTail
    }
    public UpgradeType upgradeType;
}
