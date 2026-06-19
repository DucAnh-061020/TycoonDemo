using System.Collections.Generic;
using UnityEngine;

public class UpgradeUiWindow : MonoBehaviour, IUIFlowControl
{
    [Header("Prefabs & Setup")]
    [SerializeField] private Transform slotPrefab;
    [SerializeField] private Transform slotContainer;

    [Header("Upgrade Assets Data Layer")]
    [Tooltip("Drag both your Plant and Global ScriptableObject asset files here!")]
    [SerializeField] private List<UpgradableData> _availableUpgrades;

    private List<UpgradeUISlot> activeUiSlots = new List<UpgradeUISlot>();

    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    private void Awake()
    {
#if UNITY_EDITOR
        foreach(var upgrade in _availableUpgrades)
        {
            upgrade.ResetUpgrade();
        }
#endif
        gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        _isOpen = !_isOpen;
        gameObject.SetActive(_isOpen);
    }

    private void Start()
    {
        GenerateShopLayout();
    }

    private void GenerateShopLayout()
    {
        foreach (Transform child in slotContainer) Destroy(child.gameObject);
        activeUiSlots.Clear();

        foreach (UpgradableData upgrade in _availableUpgrades)
        {
            GameObject slotObj = Instantiate(slotPrefab.gameObject, slotContainer);
            UpgradeUISlot slotScript = slotObj.GetComponent<UpgradeUISlot>();

            slotScript.RenderSlot(upgrade);
            activeUiSlots.Add(slotScript);
        }
    }
}