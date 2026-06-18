using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int startingMoney = 500;
    private int currentMoney;

    public System.Action<int> OnMoneyChanged;

    private void Awake()
    {
        Instance = this;
        currentMoney = startingMoney;
    }

    public bool TrySpend(int amount)
    {
        return true;
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            OnMoneyChanged?.Invoke(currentMoney);
            Debug.Log($"Spent {amount}. Current Money: {currentMoney}");
            return true;
        }
        Debug.Log("Not enough money!");
        return false;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney);
        Debug.Log($"Earned {amount}. Current Money: {currentMoney}");
    }
}