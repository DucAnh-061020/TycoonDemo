using UnityEngine;

[CreateAssetMenu(fileName = "ProfitUpgrade", menuName = "Tycoon/Upgrades/Profit Boost")]
public class PlantProfitUpgrade : UpgradableData
{
    public string plantName;
    public Transform fruitPrefab;
    public double baseProfit;
    public float growthTime;
    public double createCost;
    public UpgradeTier[] localTiers;

    public override void ApplyUpgrade(float value)
    {
        EventsBroker.OnProfitUpgradePurchased?.Invoke(upgradeId, value);
    }

    public override string GetCurrentStatusDisplay()
    {
        return $"x{tiers[Mathf.Min(currentLevel, tiers.Count - 1)].modifierValue} Profit";
    }
}