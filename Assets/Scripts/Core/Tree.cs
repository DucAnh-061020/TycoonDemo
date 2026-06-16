using UnityEngine;

public class Tree : MonoBehaviour, IClickable
{
    private TreeData data;
    private int currentFruits;
    private int currentLevel = 1;

    public void Initialize(TreeData treeData)
    {
        this.data = treeData;
        StartCoroutine(FruitGrowthRoutine());
    }

    private System.Collections.IEnumerator FruitGrowthRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(data.growthTime);
            if (currentFruits < data.maxFruits)
            {
                currentFruits++;
            }
        }
    }

    public int HarvestFruits()
    {
        int fruitsToGive = currentFruits;
        currentFruits = 0;
        return fruitsToGive;
    }

    public int CalculateProfit(int fruits)
    {
        return fruits * data.baseProfit * currentLevel;
    }

    public void OnClick()
    {
    }
}
