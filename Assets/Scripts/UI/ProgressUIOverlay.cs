using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUIOverlay : MonoBehaviour, IPoolableObjects
{
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private int _poolIndex = 6;
    [SerializeField] private Vector3 _offset;
    private float _progressValue = 0;

    public int PoolIndex => _poolIndex;
    public Vector3 Offset=> _offset;

    public void SetProgress(float maxTimer)
    {
        _progressValue = 0;
        _progressSlider.value = 0;
        _progressSlider.maxValue = maxTimer;
        _progressText.text = maxTimer.ToString();
    }

    private void Update()
    {
        _progressValue += Time.deltaTime;
        _progressSlider.value = _progressValue;
        _progressText.text = $"{_progressSlider.maxValue - _progressValue:N2}s";
        if (_progressValue >= _progressSlider.maxValue)
        {
            PoolManager.Instance.Release(gameObject, _poolIndex);
        }
    }
}