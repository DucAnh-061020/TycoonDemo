using UnityEngine;

public static class MovementUtility
{
    public static System.Collections.IEnumerator MoveToTarget(Transform transform, Vector3 target, float speed, float rotationSpeed = 10f)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            Vector3 direction = (target - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
        transform.position = target;
    }
}