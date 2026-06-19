using UnityEngine;

public class Tree : MonoBehaviour, IUpgradable, IPoolableObjects
{
    [Header("UI Focus Anchor")]
    [SerializeField] private Transform _uiFocusPoint;

    [Space(5)]
    [SerializeField] private int _poolIndex;
    [SerializeField] private Transform _gatherPoint;
    [SerializeField] private FruitStack _fruitStack;
    [SerializeField] private Transform _upgradeEffect;
    private PlantProfitUpgrade _data;
    private int _currentFruits;
    private int _currentLevel = 1;
    private float _currentPrice;
    public Transform GatherPoint => _gatherPoint;
    public int PoolIndex => _poolIndex;
    public FruitStack FruitStack => _fruitStack;
    public int CurrentLevel => _currentLevel;
    public int MaxLevel => _data.localTiers.Length;
    public string Name => _data.plantName;
    public float Income => _currentPrice;
    public float Growtime => _data.growthTime;
    public Vector3 UiFocusPoint => _uiFocusPoint.position;
    public Sprite ItemImage => _data.displayImage;

    public void Initialize(PlantProfitUpgrade treeData)
    {
        _data = treeData;
        _currentPrice = _data.baseProfit;
        EventsBroker.OnTreeUpdate?.Invoke(UiFocusPoint, this);
        StartCoroutine(FruitGrowthRoutine());
    }

    private System.Collections.IEnumerator FruitGrowthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_data.growthTime);
            if (_fruitStack.CanAdd(_fruitStack.MaxLimit))
            {
                Transform slot = _fruitStack.GetTargetParent();
                Vector3 localPos = _fruitStack.GetTargetPosition();

                Fruit fruitData = _data.fruitPrefab.GetComponent<Fruit>();
                GameObject fruit = PoolManager.Instance.Spawn(_uiFocusPoint, _data.fruitPrefab.gameObject, slot.position, Quaternion.identity, fruitData.PoolIndex);
                fruit.transform.position = localPos;
                Fruit fruitInfo = fruit.GetComponent<Fruit>();
                fruitInfo.SetPrice(_currentPrice);
                _fruitStack.Push(fruitInfo);
            }
        }
    }

    public int HarvestFruits()
    {
        int fruitsToGive = _currentFruits;
        _currentFruits = 0;
        return fruitsToGive;
    }

    private void OnEnable()
    {
        EventsBroker.OnProfitUpgradePurchased += HandleProfitUpgrade;
        EventsBroker.OnGlobalUpgradePurchased += HandleGlobalUpgrade;
    }

    private void OnDisable()
    {
        EventsBroker.OnProfitUpgradePurchased -= HandleProfitUpgrade;
        EventsBroker.OnGlobalUpgradePurchased -= HandleGlobalUpgrade;
    }

    private void HandleProfitUpgrade(string upgradeId, float multiplierBonus)
    {
        if (upgradeId != _data.upgradeId) return;
        _currentPrice = CalculateProfit();
        EventsBroker.OnTreeUpdate?.Invoke(UiFocusPoint, this);
    }

    private void HandleGlobalUpgrade()
    {
        _currentPrice = CalculateProfit();
        EventsBroker.OnTreeUpdate?.Invoke(UiFocusPoint, this);
    }

    public int CalculateProfit()
    {
        float localMultiply = _data.localTiers[Mathf.Min(_currentLevel - 1, _data.localTiers.Length - 1)].modifierValue;
        float shareMultiply = _data.tiers[_data.currentLevel].modifierValue;
        float baseCalculation = _data.baseProfit * localMultiply
            * shareMultiply * GlobalUpgradeManager.Instance.GlobalProfitMultiply;
        return Mathf.RoundToInt(baseCalculation);
    }

    public void Execute()
    {
        if (_currentLevel < _data.localTiers.Length && CurrencyManager.Instance.TrySpend(_data.localTiers[_currentLevel].cost))
        {
            Upgrade();
            EventsBroker.OnTreeUpdate?.Invoke(UiFocusPoint, this);
        }
    }

    private void Upgrade()
    {
        _currentLevel++;
        _currentPrice = CalculateProfit();
        _fruitStack.UpdateNewPrice(_currentPrice);
        if (_upgradeEffect.TryGetComponent<VfxEffect>(out var effect))
        {
            PoolManager.Instance.Spawn(_upgradeEffect.gameObject, transform.position, Quaternion.identity, effect.PoolIndex);
        }
    }
}
