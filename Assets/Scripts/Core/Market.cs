using UnityEngine;

public class Market : MonoBehaviour, IStorageLocation
{
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform tradingPoint;

    public Transform InteractionPoint => tradingPoint;
    public Transform EntryPoint => entryPoint;
    public Transform ExitPoint => exitPoint;

    private int totalFruitsInMarket;

    public void DepositFruits(int amount)
    {
        totalFruitsInMarket += amount;
        // Process cash reward immediately or wait for buyer
    }

    public int WithdrawFruits(int amount)
    {
        int taken = Mathf.Min(amount, totalFruitsInMarket);
        totalFruitsInMarket -= taken;
        return taken;
    }
}
