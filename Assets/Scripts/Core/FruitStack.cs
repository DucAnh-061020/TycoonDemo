using System.Collections.Generic;
using UnityEngine;

public class FruitStack : MonoBehaviour
{
    [SerializeField] private Transform[] fixedSlots;

    [Tooltip("Used by agents (Deliverer/Customer) to stack upwards")]

    private Stack<Fruit> stack = new Stack<Fruit>();

    public int Count => stack.Count;
    public int MaxLimit => fixedSlots.Length;

    public bool CanAdd(int maxLimit)
    {
        return stack.Count < maxLimit;
    }

    public Transform GetTargetParent()
    {
        int index = Mathf.Min(stack.Count, fixedSlots.Length - 1);
        return fixedSlots[index];
    }

    public Vector3 GetTargetPosition()
    {
        int index = Mathf.Min(stack.Count, fixedSlots.Length - 1);
        return fixedSlots[index].position;
    }

    public void Push(Fruit fruit) => stack.Push(fruit);

    public Fruit Pop()
    {
        if (stack.Count > 0) return stack.Pop();
        return null;
    }

    public void ClearAndDestroyStack()
    {
        while (stack.Count > 0)
        {
            Fruit fruit = stack.Pop();
            if (fruit != null)
            {
                fruit.transform.SetParent(null);
                PoolManager.Instance.Release(fruit.gameObject, fruit.PoolIndex);
            }
        }
    }
}