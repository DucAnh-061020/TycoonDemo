using UnityEngine;

public class Tree : MonoBehaviour, IClickable, IPoolableObjects
{
    [SerializeField] private int _poolIndex;
    [SerializeField] private Transform _gatherPoint;
    [SerializeField] private FruitStack _fruitStack;
    private TreeData _data;
    private int _currentFruits;
    private int _currentLevel = 1;
    private float _currentPrice;
    public Transform GatherPoint => _gatherPoint;
    public int PoolIndex => _poolIndex;
    public FruitStack FruitStack => _fruitStack;

    public void Initialize(TreeData treeData)
    {
        _data = treeData;
        _currentPrice = _data.baseProfit;
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
                GameObject fruit = PoolManager.Instance.Spawn(_data.fruitPrefab.gameObject, slot.position, Quaternion.identity,fruitData.PoolIndex);
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

    public void OnClick()
    {
        _currentLevel++;
        _currentPrice = _currentPrice + _data.baseProfit * 0.1f;
    }

    private float typeProfitMultiplier = 1.0f;
    private static float globalProfitMultiplier = 1.0f;

    private void OnEnable()
    {
        EventsBroker.OnProfitUpgradePurchased += HandleProfitUpgrade;
    }

    private void OnDisable()
    {
        EventsBroker.OnProfitUpgradePurchased -= HandleProfitUpgrade;
    }

    private void HandleProfitUpgrade(string targetType, float multiplierBonus)
    {
        if (targetType == "Global")
        {
            globalProfitMultiplier += multiplierBonus;
        }
        else if (_data != null && _data.treeType == targetType)
        {
            typeProfitMultiplier += multiplierBonus;
        }
    }

    public int CalculateProfit(int fruits)
    {
        float baseCalculation = fruits * _data.baseProfit * _currentLevel;
        return Mathf.RoundToInt(baseCalculation * typeProfitMultiplier * globalProfitMultiplier);
    }
}
