using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUISlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Button purchaseButton;

    private UpgradableData linkedUpgrade;

    private void Awake()
    {
        purchaseButton.onClick.AddListener(OnPurchaseSlotClicked);
    }

    public void RenderSlot(UpgradableData upgrade)
    {
        linkedUpgrade = upgrade;

        titleText.text = upgrade.title;
        descriptionText.text = $"{upgrade.description} {upgrade.GetCurrentStatusDisplay()}";

        if (upgrade.IsMaxLevel())
        {
            costText.text = "MAX";
            purchaseButton.interactable = false;
        }
        else
        {
            UpgradeTier nextTier = upgrade.GetNextTier();
            costText.text = $"${nextTier.cost}";
            purchaseButton.interactable = true;
        }
        upgradeImage.sprite = upgrade.displayImage;
    }

    private void OnPurchaseSlotClicked()
    {
        if (linkedUpgrade == null || linkedUpgrade.IsMaxLevel()) return;

        UpgradeTier nextTier = linkedUpgrade.GetNextTier();

        if (CurrencyManager.Instance.TrySpend(nextTier.cost))
        {
            linkedUpgrade.currentLevel++;

            linkedUpgrade.ApplyUpgrade(nextTier.modifierValue);

            RenderSlot(linkedUpgrade);
        }
    }
}