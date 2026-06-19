using UnityEngine;

public class Fruit : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private int _poolIndex;
    [SerializeField] private double _sellPrice;
    public int PoolIndex => _poolIndex;
    public double SellPrice => _sellPrice;
    public void SetPrice(double price)
    {
        _sellPrice = price;
    }
}