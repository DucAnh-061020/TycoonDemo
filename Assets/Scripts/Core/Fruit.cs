using UnityEngine;

public class Fruit : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private int _poolIndex;
    [SerializeField] private float _sellPrice;
    public int PoolIndex => _poolIndex;
    public float SellPrice => _sellPrice;
    public void SetPrice(float price)
    {
        _sellPrice = price;
    }
}