using UnityEngine;

public static class EventsBroker
{
    public static System.Action<string, float> OnProfitUpgradePurchased;
    public static System.Action OnGlobalUpgradePurchased;
    public static System.Action<PlantProfitUpgrade, Transform> OnTreeUnlocked;
    public static System.Action<Vector3, Tree> OnTreeUpdate;
    public static System.Action<Vector3, float> OnBuildingStart;
}
