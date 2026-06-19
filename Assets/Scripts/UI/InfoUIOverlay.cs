using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoUIOverlay : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private TextMeshProUGUI _income;
    [SerializeField] private TextMeshProUGUI _incomePerMinutes;
    [SerializeField] private Image _productImage;
    [SerializeField] private int _poolIndex = 7;
    [SerializeField] private Vector3 _offset;
    public int PoolIndex => _poolIndex;
    public Vector3 Offset => _offset;
    public void UpdateOverlay(double profit, float growthSpeed, Sprite productImage)
    {
        _income.text = $"{CurrencyFormatter.FormatValue(profit)}";
        _incomePerMinutes.text = $"{CurrencyFormatter.FormatValue(profit * growthSpeed)}/min";
        _productImage.sprite = productImage;
    }
}