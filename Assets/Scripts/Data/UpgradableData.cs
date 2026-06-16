// The base for all upgrade types
using UnityEngine;

public abstract class UpgradableData : ScriptableObject
{
    public string upgradeName;
    public int baseCost;
    public float costMultiplier;

    // Strategy Pattern: Each upgrade defines its own application logic
    public abstract void ApplyUpgrade();
}