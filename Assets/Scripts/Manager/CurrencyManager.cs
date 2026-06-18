using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [SerializeField] private int startingMoney = 500;
    private float currentMoney;
    public float CurrentMoney => currentMoney;

    public event System.Action<float> OnMoneyChanged;

    private void Awake()
    {
        Instance = this;
        currentMoney = startingMoney;
    }

    public bool TrySpend(float amount)
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

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        OnMoneyChanged?.Invoke(currentMoney);
        Debug.Log($"Earned {amount}. Current Money: {currentMoney}");
    }
}