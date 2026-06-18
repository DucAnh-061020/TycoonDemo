using UnityEngine;

public class Tree : MonoBehaviour, IClickable, IPoolableObjects
{
    [SerializeField] private int _poolIndex;
    [SerializeField] private Transform _gatherPoint;
    [SerializeField] private Transform[] _fruitHolders;
    private TreeData _data;
    private int _currentFruits;
    private int _currentLevel = 1;
    public Transform GatherPoint => _gatherPoint;
    public int PoolIndex => _poolIndex;

    public void Initialize(TreeData treeData)
    {
        _data = treeData;
        StartCoroutine(FruitGrowthRoutine());
    }

    private System.Collections.IEnumerator FruitGrowthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_data.growthTime);
            if (_currentFruits < _data.maxFruits)
            {
                _currentFruits++;
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
    }

    // Add these lines to your existing Tree script:

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
