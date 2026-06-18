using UnityEngine;

[CreateAssetMenu(fileName = "NewTreeData", menuName = "Tycoon/Tree Data")]
public class TreeData : ScriptableObject
{
    public string treeType;
    public Transform fruitPrefab;
    public int baseProfit;
    public int maxFruits;
    public float growthTime;
}
