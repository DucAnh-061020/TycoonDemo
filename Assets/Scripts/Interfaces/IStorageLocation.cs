using UnityEngine;

public interface IStorageLocation
{
    Transform WaitingPoint { get; }
    Transform DeliverPoint { get; }
    void DepositFruits(int amount);
    int WithdrawFruits(int amount);
}