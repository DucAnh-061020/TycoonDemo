using UnityEngine;

public class UnlockableSlot : MonoBehaviour, IClickable
{
    [SerializeField] private TreeData treeData;
    [SerializeField] private float unlockTime = 5f;
    private bool isUnlocking = false;
    private bool isUnlocked = false;

    public static event System.Action<TreeData, Transform> OnTreeUnlocked;

    public void OnClick()
    {
        if (!isUnlocking && !isUnlocked && CurrencyManager.Instance.TrySpend(100))
        {
            StartCoroutine(UnlockSequence());
        }
    }

    private System.Collections.IEnumerator UnlockSequence()
    {
        isUnlocking = true;
        yield return new WaitForSeconds(unlockTime);

        isUnlocked = true;
        isUnlocking = false;

        OnTreeUnlocked?.Invoke(treeData, this.transform);
        Destroy(gameObject);
    }
}