using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContructionUI : MonoBehaviour, IUIFlowControl
{
    [SerializeField] private TextMeshProUGUI _productNameText;
    [SerializeField] private TextMeshProUGUI _unlockPriceText;
    [SerializeField] private Button _unlockButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private Image _itemImage;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        _closeButton.onClick.AddListener(() =>
        {
            ToggleMenu();
        });
        gameObject.SetActive(false);
    }

    public void SetUnlockInfo(string name, float price, Vector3 position, Sprite itemImage, System.Action callBack)
    {
        if (_isOpen) return;
        ToggleMenu();
        _unlockButton.onClick.RemoveAllListeners();
        _unlockButton.onClick.AddListener(() =>
        {
            callBack?.Invoke();
            ToggleMenu();
        });
        _itemImage.sprite = itemImage;
        gameObject.SetActive(true);
        _productNameText.text = name;
        _unlockPriceText.text = price.ToString();
        _rectTransform.anchoredPosition = _offset + UICoordinateUtility.WorldToOverlayCanvasPosition(position, Camera.main, _canvas);
    }

    public void ToggleMenu()
    {
        _isOpen = !_isOpen;
        gameObject.SetActive(_isOpen);
    }
}