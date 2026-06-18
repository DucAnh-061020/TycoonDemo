using UnityEngine;

public class CustomerAI : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private int _poolIndex;
    [SerializeField] private AgentVisuals _visuals;
    [SerializeField] private FruitStack _fruitStack;
    [SerializeField] private Transform _collectCoinEffect;
    private Market _market;
    private Dock _assignedDock;
    private bool _isServed = false;
    public int PoolIndex => _poolIndex;
    public FruitStack FruitStack => _fruitStack;

    public void InitializeFromPool(Market market)
    {
        this._market = market;
        this._assignedDock = market.GetAvailableDock();
        this._isServed = false;

        _fruitStack.ClearAndDestroyStack();
        transform.position = market.EntryPoint.position;
        StartCoroutine(CustomerRoutine());
    }

    private System.Collections.IEnumerator CustomerRoutine()
    {
        _visuals.SetMovement(true);
        _visuals.SetPocket(true);
        yield return MovementUtility.MoveToTarget(transform, _assignedDock.WaitingPoint.position, _moveSpeed);
        transform.LookAt(_assignedDock.DeliverPoint);
        _assignedDock.RegisterCustomer(this);
        _visuals.SetMovement(false);
        while (!_isServed)
        {
            yield return new WaitForEndOfFrame();
        }
        _visuals.SetMovement(true);
        _visuals.SetPocket(false);
        GameDirector.Instance.NotifyCustomerLeft();
        yield return MovementUtility.MoveToTarget(transform, _market.ExitPoint.position, _moveSpeed);
        PoolManager.Instance.Release(gameObject, _poolIndex);
    }

    public void ReceiveOrder()
    {
        _isServed = true;
        if (_collectCoinEffect.TryGetComponent<VfxEffect>(out var effect))
        {
            PoolManager.Instance.Spawn(_collectCoinEffect.gameObject, transform.position, Quaternion.identity, effect.PoolIndex);
        }
    }
}