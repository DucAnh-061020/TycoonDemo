using UnityEngine;

public class UnlockableSlot : MonoBehaviour, IUnlockable
{
    [SerializeField] private PlantProfitUpgrade _treeData;
    [SerializeField] private float _unlockTime = 5f;
    [SerializeField] private Animation _animation;
    [SerializeField] private Transform _uiFocusPoint;
    [SerializeField] private Transform _onSpawnEffect;
    private bool _isUnlocking = false;
    private bool _isUnlocked = false;

    public string Name => _treeData.plantName;
    public float UnlockPrice => _treeData.createCost;
    public Vector3 UiFocusPoint => _uiFocusPoint.position;
    public Sprite ItemImage => _treeData.displayImage;

    public void Execute()
    {
        if (!_isUnlocking && !_isUnlocked && CurrencyManager.Instance.TrySpend(_treeData.createCost))
        {
            _animation.Play("BoxOpen");
            EventsBroker.OnBuildingStart?.Invoke(_uiFocusPoint.position, _unlockTime);
            StartCoroutine(UnlockSequence());
        }
    }

    private System.Collections.IEnumerator UnlockSequence()
    {
        _isUnlocking = true;
        yield return new WaitForSeconds(_unlockTime);

        _isUnlocked = true;
        _isUnlocking = false;

        EventsBroker.OnTreeUnlocked?.Invoke(_treeData, transform);
        if(_onSpawnEffect.TryGetComponent<VfxEffect>(out var effect))
        {
            PoolManager.Instance.Spawn(_onSpawnEffect.gameObject, transform.position, Quaternion.identity, effect.PoolIndex);
        }
        Destroy(gameObject);
    }
}