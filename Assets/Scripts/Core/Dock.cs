using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField] private Transform waitingPoint;
    [SerializeField] private Transform deliverPoint;

    private Queue<CustomerAI> waitingCustomers = new Queue<CustomerAI>();
    private int reservedCustomerCount = 0;

    public Transform WaitingPoint => waitingPoint;
    public Transform DeliverPoint => deliverPoint;
    public bool HasWaitingCustomer => waitingCustomers.Count > 0;

    public void RegisterCustomer(CustomerAI customer)
    {
        if (!waitingCustomers.Contains(customer))
            waitingCustomers.Enqueue(customer);
    }

    public bool TryReserveCustomer()
    {
        if(waitingCustomers.Count > reservedCustomerCount)
        {
            reservedCustomerCount++;
            return true;
        }
        return false;
    }

    public bool TrySellFruits(int amount, out int profitGenerated)
    {
        profitGenerated = 0;
        if (!HasWaitingCustomer) return false;

        CustomerAI currentCustomer = waitingCustomers.Dequeue();

        reservedCustomerCount = Mathf.Max(0, reservedCustomerCount - 1);

        profitGenerated = amount * 15;

        currentCustomer.ReceiveOrder();
        return true;
    }
}