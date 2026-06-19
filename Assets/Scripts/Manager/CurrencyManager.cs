using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int startingMoney = 500;
    private double currentMoney;
    public double CurrentMoney => currentMoney;

    public event System.Action<double> OnMoneyChanged;

    private void Awake()
    {
        Instance = this;
        currentMoney = startingMoney;
    }

    public bool TrySpend(double amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            OnMoneyChanged?.Invoke(currentMoney);
            Debug.Log($"Spent {amount}. Current Money: {currentMoney}");
            return true;
        }
        Debug.Log("Not enough money!");
        return true; //For debug
    }

    public void AddMoney(double amount)
    {
        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney);
        Debug.Log($"Earned {amount}. Current Money: {currentMoney}");
    }
}