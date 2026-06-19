// The base for all upgrade types
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradableData : ScriptableObject
{
    [Header("General info")]
    public string upgradeId;
    public string title;
    [TextArea] public string description;

    [Header("Tiers")]
    public List<UpgradeTier> tiers;
    [HideInInspector] public int currentLevel = 0;
    public bool IsMaxLevel() => currentLevel >= tiers.Count;
    public UpgradeTier GetNextTier() => IsMaxLevel() ? null : tiers[currentLevel];
    public Sprite displayImage;

    public abstract void ApplyUpgrade(float value);
    public abstract string GetCurrentStatusDisplay();
#if UNITY_EDITOR
    public void ResetUpgrade()
    {
        currentLevel = 0;
    }
#endif
}

[System.Serializable]
public class UpgradeTier
{
    public double cost;
    public float modifierValue;
}