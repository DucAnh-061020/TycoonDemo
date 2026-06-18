using UnityEngine;

public class Fruit : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private int _poolIndex;
    public int PoolIndex => _poolIndex;
}