using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvas : MonoBehaviour
{
    private Transform _cameraTransform;
    [SerializeField] private Transform _progressPrefab;
    [SerializeField] private Transform _productPrefab;
    Dictionary<int, InfoUIOverlay> _overlayInfos = new();

    private void Awake()
    {
        if (Camera.main != null)
        {
            _cameraTransform = Camera.main.transform;
        }
        if (_cameraTransform == null) return;

        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward,
                         _cameraTransform.rotation * Vector3.up);

        EventsBroker.OnTreeUpdate += UpdateTreeInfo;
        EventsBroker.OnBuildingStart += SetBuildingInfo;
    }

    private void OnDestroy()
    {
        EventsBroker.OnTreeUpdate -= UpdateTreeInfo;
        EventsBroker.OnBuildingStart -= SetBuildingInfo;
    }

    private void SetBuildingInfo(Vector3 position, float buildTime)
    {
        if (!_progressPrefab.TryGetComponent<ProgressUIOverlay>(out var progressUI))
        {
            return;
        }
        var progressObject = PoolManager.Instance.Spawn(transform, progressUI.gameObject, position + progressUI.Offset, transform.rotation, progressUI.PoolIndex);
        var newProgress = progressObject.GetComponent<ProgressUIOverlay>();
        newProgress.SetProgress(buildTime);
    }

    private void UpdateTreeInfo(Vector3 position, Tree tree)
    {
        if (_overlayInfos.TryGetValue(tree.GetInstanceID(), out var cacheInfoUI))
        {
            cacheInfoUI.UpdateOverlay(tree.Income, tree.Growtime, tree.ItemImage);
            return;
        }
        if (!_productPrefab.TryGetComponent<InfoUIOverlay>(out var infoUI))
        {
            return;
        }
        var infoObject = PoolManager.Instance.Spawn(transform, infoUI.gameObject, position + infoUI.Offset, transform.rotation, infoUI.PoolIndex);
        var newInfo = infoObject.GetComponent<InfoUIOverlay>();
        newInfo.UpdateOverlay(tree.Income, tree.Growtime, tree.ItemImage);
        _overlayInfos.Add(tree.GetInstanceID(), newInfo);
    }
}