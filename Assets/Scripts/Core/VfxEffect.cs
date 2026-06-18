using UnityEngine;

public class VfxEffect : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private int _poolIndex;
    [SerializeField] private float _timeToLive;
    public int PoolIndex => _poolIndex;
    private float _deactiveTime;

    private void OnEnable()
    {
        _deactiveTime = 0;
    }

    private void Update()
    {
        _deactiveTime += Time.deltaTime;
        if (_deactiveTime >= _timeToLive)
            PoolManager.Instance.Release(gameObject, _poolIndex);
    }
}