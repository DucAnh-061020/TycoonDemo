using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductUpgradeUI : MonoBehaviour, IUIFlowControl
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _productNameText;
    [SerializeField] private TextMeshProUGUI _incomeText;
    [SerializeField] private TextMeshProUGUI _growText;
    [SerializeField] private Slider _levelProgressBar;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private Button _upgradeBtn;
    [SerializeField] private Transform _maxBtn;
    [SerializeField] private RectTransform _canvas;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        _closeBtn.onClick.AddListener(() =>
        {
            ToggleMenu();
        });
        gameObject.SetActive(false);
    }

    public void SetData(int currentLevel, int maxLevel, string productName, float income, float growTime, Vector3 worldPosition, System.Action callback)
    {
        if (_isOpen) return;
        ToggleMenu();
        _upgradeBtn.gameObject.SetActive(maxLevel > currentLevel);
        _maxBtn.gameObject.SetActive(maxLevel == currentLevel);
        _upgradeBtn.onClick.RemoveAllListeners();
        _upgradeBtn.onClick.AddListener(() =>
        {
            ToggleMenu();
            callback?.Invoke();
        });
        _growText.text = growTime.ToString();
        _incomeText.text = income.ToString();
        _levelText.text = currentLevel.ToString();
        _productNameText.text = productName;
        _levelProgressBar.maxValue = maxLevel;
        _levelProgressBar.value = currentLevel;
        _levelProgressBar.minValue = 1;
        _rectTransform.anchoredPosition = UICoordinateUtility.WorldToOverlayCanvasPosition(worldPosition, Camera.main, _canvas) + _offset;
    }

    public void ToggleMenu()
    {
        _isOpen = !_isOpen;
        gameObject.SetActive(_isOpen);
    }
}