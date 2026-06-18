using UnityEngine;

public class GeneralUpgradeUI : MonoBehaviour, IUIFlowControl
{
    [SerializeField] private Transform _optionHolder;
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ToggleMenu()
    {
        _isOpen = !_isOpen;
        gameObject.SetActive(_isOpen);
    }
}