using UnityEngine;

public class CustomerAI : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private int _poolIndex;
    [SerializeField] private AgentVisuals _visuals;
    private Market _market;
    private Dock _assignedDock;
    private bool _isServed = false;
    public int PoolIndex => _poolIndex;

    public void InitializeFromPool(Market market)
    {
        this._market = market;
        this._assignedDock = market.GetAvailableDock();
        this._isServed = false;

        transform.position = market.EntryPoint.position;
        StartCoroutine(CustomerRoutine());
    }

    private System.Collections.IEnumerator CustomerRoutine()
    {
        _visuals.SetMovement(true);
        _visuals.SetPocket(true);
        yield return MovementUtility.MoveToTarget(transform, _assignedDock.WaitingPoint.position, _moveSpeed);

        _assignedDock.RegisterCustomer(this);
        _visuals.SetMovement(false);
        while (!_isServed)
        {
            yield return new WaitForSeconds(0.2f);
        }
        _visuals.SetMovement(true);
        _visuals.SetPocket(true);
        GameDirector.Instance.NotifyCustomerLeft();
        yield return MovementUtility.MoveToTarget(transform, _market.ExitPoint.position, _moveSpeed);
        PoolManager.Instance.Release(gameObject, _poolIndex);
    }

    public void ReceiveOrder()
    {
        _isServed = true;
    }
}