using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    private int currentBalance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public bool TrySpend(int amount)
    {
        if (currentBalance >= amount)
        {
            currentBalance -= amount;
            return true;
        }
        return false;
    }
}