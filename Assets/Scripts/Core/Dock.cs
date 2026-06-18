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

    public void RegisterCustomer(CustomerAI customer)
    {
        if (!waitingCustomers.Contains(customer))
            waitingCustomers.Enqueue(customer);
    }

    public CustomerAI PrepareTransaction()
    {
        if (waitingCustomers.Count > 0)
        {
            reservedCustomerCount = Mathf.Max(0, reservedCustomerCount - 1);
            return waitingCustomers.Dequeue();
        }
        return null;
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
}