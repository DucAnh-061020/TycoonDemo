using UnityEngine;

public interface IStorageLocation
{
    Transform InteractionPoint { get; }
    void DepositFruits(int amount);
    int WithdrawFruits(int amount);
}