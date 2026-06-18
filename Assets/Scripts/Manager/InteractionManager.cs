using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private float _maxRaycastDst = 100;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TriggerClickDetection();
        }
    }

    private void TriggerClickDetection()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, _maxRaycastDst))
        {
            return;
        }

        if (!hit.collider.TryGetComponent<ISelectableTarget>(out var selectableTarget))
        {
            return;
        }

        if (hit.collider.TryGetComponent(out IUpgradable upgradable))
        {
            OverlayCanvasManager.Instance.UpgradeUI.SetData(upgradable.CurrentLevel, upgradable.MaxLevel, upgradable.Name, upgradable.Income, upgradable.Growtime, upgradable.UiFocusPoint, upgradable.Execute);
            return;
        }

        if (hit.collider.TryGetComponent(out IUnlockable unlockable))
        {
            OverlayCanvasManager.Instance.ContructionUI.SetUnlockInfo(unlockable.Name, unlockable.UnlockPrice, unlockable.UiFocusPoint, unlockable.ItemImage, unlockable.Execute);
            return;
        }
    }
}