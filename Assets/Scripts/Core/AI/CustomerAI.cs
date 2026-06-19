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
    private bool _isBuying = false;
    public int PoolIndex => _poolIndex;
    public FruitStack FruitStack => _fruitStack;
    public bool IsBuying => _isBuying;
    public void InitializeFromPool(Market market)
    {
        _market = market;
        _assignedDock = market.GetAvailableDock();
        _isServed = false;
        _isBuying = false;
        _fruitStack.ClearAndDestroyStack();
        transform.position = market.EntryPoint.position;
        StartCoroutine(CustomerRoutine());
    }

    private System.Collections.IEnumerator CustomerRoutine()
    {
        _visuals.SetMovement(true);
        _visuals.SetPocket(true);
        _assignedDock.RegisterCustomer(this);
        _isBuying = false;
        yield return MovementUtility.MoveToTarget(transform, _assignedDock.WaitingPoint.position, _moveSpeed);
        _isBuying = true;
        transform.LookAt(_assignedDock.DeliverPoint);
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