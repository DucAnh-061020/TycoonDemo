using UnityEngine;

public class UnlockableSlot : MonoBehaviour, IClickable
{
    [SerializeField] private TreeData _treeData;
    [SerializeField] private float _unlockTime = 5f;
    [SerializeField] private Animation _animation;
    private bool isUnlocking = false;
    private bool isUnlocked = false;

    public static event System.Action<TreeData, Transform> OnTreeUnlocked;

    public void OnClick()
    {
        if (!isUnlocking && !isUnlocked && CurrencyManager.Instance.TrySpend(100))
        {
            _animation.Play("BoxOpen");
            StartCoroutine(UnlockSequence());
        }
    }

    private System.Collections.IEnumerator UnlockSequence()
    {
        isUnlocking = true;
        yield return new WaitForSeconds(_unlockTime);

        isUnlocked = true;
        isUnlocking = false;

        OnTreeUnlocked?.Invoke(_treeData, this.transform);
        Destroy(gameObject);
    }
}