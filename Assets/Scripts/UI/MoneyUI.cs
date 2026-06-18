using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coin;

    private void Start()
    {
        CurrencyManager.Instance.OnMoneyChanged += Instance_OnMoneyChanged;
        _coin.text = CurrencyManager.Instance.CurrentMoney.ToString();
    }

    private void OnDestroy()
    {
        CurrencyManager.Instance.OnMoneyChanged -= Instance_OnMoneyChanged;
    }

    private void Instance_OnMoneyChanged(float newValue)
    {
        _coin.text = newValue.ToString();
    }
}