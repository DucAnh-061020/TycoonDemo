using System.Collections;
using UnityEngine;

public static class FruitTransferUtility
{
    public static IEnumerator TransferRoutine(Fruit fruit, FruitStack targetStack, float moveSpeed)
    {
        Transform targetParent = targetStack.GetTargetParent();

        Vector3 worldTarget = targetStack.GetTargetPosition();

        fruit.transform.SetParent(null);

        while (Vector3.Distance(fruit.transform.position, worldTarget) > 0.02f)
        {
            worldTarget = targetStack.GetTargetPosition();
            fruit.transform.position = Vector3.MoveTowards(fruit.transform.position, worldTarget, moveSpeed * Time.deltaTime);
            fruit.transform.rotation = Quaternion.Slerp(fruit.transform.rotation, targetParent.rotation, 15f * Time.deltaTime);
            yield return null;
        }

        Vector3 finalWorldPosition = targetStack.GetTargetPosition();
        Quaternion finalWorldRotation = targetParent.rotation;

        fruit.transform.SetParent(targetParent);

        fruit.transform.position = finalWorldPosition;
        fruit.transform.rotation = finalWorldRotation;

        targetStack.Push(fruit);
    }
}