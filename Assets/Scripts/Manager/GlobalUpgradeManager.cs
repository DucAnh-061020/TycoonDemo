using UnityEngine;

public class GlobalUpgradeManager:MonoBehaviour
{
    public static GlobalUpgradeManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}