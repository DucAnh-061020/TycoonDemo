using UnityEngine;

public class GlobalUpgradeManager : MonoBehaviour
{
    public static GlobalUpgradeManager Instance { get; private set; }
    private int _maxCustomer = 1;
    private float _globalProfitMultiply = 1f;
    public int MaxCustomer => _maxCustomer;
    public float GlobalProfitMultiply => _globalProfitMultiply;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMaxCustomersFromUpgrade(int value)
    {
        _maxCustomer = value;
    }

    public void UpdateGlobalMultiplierFromUpgrade(float value)
    {
        _globalProfitMultiply = value;
    }
}