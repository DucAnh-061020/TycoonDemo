using UnityEngine;

[CreateAssetMenu(fileName = "NewTreeData", menuName = "Tycoon/Tree Data")]
public class TreeData : ScriptableObject
{
    public string treeType;
    public Transform fruitPrefab;
    public float baseProfit;
    public float growthTime;
    public float upgradeCost;
    public float createCost;
    public int maxLevel;
    public Sprite productImage;
}
