using UnityEngine;

public class DelivererAI : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _poolIndex = 1; 
    [SerializeField] private AgentVisuals _visuals;
    [SerializeField] private FruitStack _fruitStack;
    private Market _market;
    private Dock _targetDock;
    private Tree _targetTree;
    private bool _shouldRetire = false;

    public int PoolIndex => _poolIndex;

    public void InitializeFromPool(Market market, Tree associatedTree)
    {
        this._market = market;
        this._targetTree = associatedTree;
        this._shouldRetire = false;
        _visuals.SetPocket(true);
        _visuals.SetMovement(false);
        StartCoroutine(DelivererRoutine());
    }

    private System.Collections.IEnumerator DelivererRoutine()
    {
        while (!_shouldRetire)
        {
            if (_fruitStack.Count == 0)
            {
                _visuals.SetPocket(true);
                _visuals.SetMovement(true);
                yield return MovementUtility.MoveToTarget(transform, _targetTree.GatherPoint.position, _moveSpeed);
                transform.LookAt(_targetTree.transform);
                _visuals.SetMovement(false);

                while (_fruitStack.Count < _fruitStack.MaxLimit)
                {
                    if (_targetTree.FruitStack.Count > 0)
                    {
                        var fruit = _targetTree.FruitStack.Pop();
                        yield return StartCoroutine(FruitTransferUtility.TransferRoutine(fruit, _fruitStack, 8f));
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.2f);
                        if (_targetTree.FruitStack.Count == 0 && _fruitStack.Count > 0) break;
                    }
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
            transform.LookAt(_targetDock.WaitingPoint);
            _visuals.SetMovement(false);

            CustomerAI customerToServe = _targetDock.PrepareTransaction();
            if (customerToServe != null)
            {
                while (_fruitStack.Count > 0)
                {
                    Fruit fruit = _fruitStack.Pop();
                    CurrencyManager.Instance.AddMoney(fruit.SellPrice);
                    yield return StartCoroutine(FruitTransferUtility.TransferRoutine(fruit, customerToServe.FruitStack, 12f));
                }

                customerToServe.ReceiveOrder();
                _shouldRetire = true;
            }

            yield return new WaitForEndOfFrame();
        }
        _visuals.SetMovement(true);
        _visuals.SetPocket(true);
        GameDirector.Instance.NotifyDelivererRetired(_targetTree);
        yield return MovementUtility.MoveToTarget(transform, _market.RetirePoint.position, _moveSpeed);
        PoolManager.Instance.Release(gameObject, _poolIndex);
    }
}