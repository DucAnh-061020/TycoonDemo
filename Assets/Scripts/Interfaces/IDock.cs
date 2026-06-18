using UnityEngine;

public interface IDock
{
    Transform DeliverPoint { get; }
    Transform WaitingPoint { get; }
    bool HasWaitingCustomer { get; }

    void RegisterCustomer(CustomerAI customer);
    void UnregisterCustomer(CustomerAI customer);
    bool TrySellFruits(int amount, out int profitGenerated);
}
