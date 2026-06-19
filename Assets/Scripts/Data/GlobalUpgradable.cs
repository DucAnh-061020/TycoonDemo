using UnityEngine;

public enum GlobalUpgradeType { MaxCustomers, GlobalProfit }

[CreateAssetMenu(fileName = "NewGlobalUpgrade", menuName = "Tycoon/Upgrades/Global Value")]
public class GlobalUpgradable : UpgradableData
{
    public GlobalUpgradeType globalType;

    public override void ApplyUpgrade(float value)
    {
        switch (globalType)
        {
            case GlobalUpgradeType.MaxCustomers:
                GlobalUpgradeManager.Instance.UpdateMaxCustomersFromUpgrade((int)value);
                break;
            case GlobalUpgradeType.GlobalProfit:
                GlobalUpgradeManager.Instance.UpdateGlobalMultiplierFromUpgrade(value);
                break;
        }
        EventsBroker.OnGlobalUpgradePurchased?.Invoke();
    }

    public override string GetCurrentStatusDisplay()
    {
        switch (globalType)
        {
            default:
            case GlobalUpgradeType.MaxCustomers:
                return $"{tiers[Mathf.Min(currentLevel, tiers.Count - 1)].modifierValue} Total Customers";
            case GlobalUpgradeType.GlobalProfit:
                return $"x{tiers[Mathf.Min(currentLevel, tiers.Count - 1)].modifierValue} Global Income";
        }
    }
}