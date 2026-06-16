using UnityEngine;

[CreateAssetMenu(fileName = "ProfitUpgrade", menuName = "Tycoon/Upgrades/Profit Boost")]
public class ProfitUpgradeData : UpgradableData
{
    public string targetTreeType; // Set to "Global" or a specific type like "Apple"
    public float multiplierIncrease;

    public override void ApplyUpgrade()
    {
        EventsBroker.OnProfitUpgradePurchased?.Invoke(targetTreeType, multiplierIncrease);
    }
}