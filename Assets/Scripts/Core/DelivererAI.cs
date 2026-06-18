using UnityEngine;

public class DelivererAI : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _poolIndex = 1; 
    [SerializeField] private AgentVisuals _visuals;
    private Market _market;
    private Dock _targetDock;
    private Tree _targetTree;

    private int _inventory = 0;
    private bool _shouldRetire = false;

    public int PoolIndex => _poolIndex;

    public void InitializeFromPool(Market market, Tree associatedTree)
    {
        this._market = market;
        this._targetTree = associatedTree;
        //this._targetDock = market.GetAvailableDock();
        this._shouldRetire = false;
        this._inventory = 0;

        StartCoroutine(DelivererRoutine());
    }

    private System.Collections.IEnumerator DelivererRoutine()
    {
        while (!_shouldRetire)
        {
            if (_inventory == 0)
            {
                _visuals.SetPocket(true);
                _visuals.SetMovement(true);
                yield return MovementUtility.MoveToTarget(transform, _targetTree.GatherPoint.position, _moveSpeed);
                _visuals.SetMovement(false);
                while (_inventory == 0)
                {
                    _inventory = _targetTree.HarvestFruits();
                    if (_inventory == 0) yield return new WaitForSeconds(0.5f);
                }
            }
            _visuals.SetPocket(false);
            _targetDock = null;
            while (!_market.TryReserveAnyCustomer(out _targetDock))
            {
                yield return new WaitForSeconds(0.2f);
            }
            _visuals.SetMovement(true);
            yield return MovementUtility.MoveToTarget(transform, _targetDock.DeliverPoint.position, _moveSpeed);
            _visuals.SetMovement(false);
            if (_targetDock.TrySellFruits(_inventory, out int cashEarned))
            {
                CurrencyManager.Instance.AddMoney(cashEarned);
                _inventory = 0;
                _shouldRetire = true;
            }

            yield return new WaitForSeconds(0.2f);
        }
        _visuals.SetMovement(true);
        _visuals.SetPocket(true);
        GameDirector.Instance.NotifyDelivererRetired(_targetTree);
        yield return MovementUtility.MoveToTarget(transform, _market.RetirePoint.position, _moveSpeed);
        PoolManager.Instance.Release(gameObject, _poolIndex);
    }
}