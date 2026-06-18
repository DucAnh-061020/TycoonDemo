using UnityEngine;

public class OverlayCanvasManager : MonoBehaviour
{
    public static OverlayCanvasManager Instance { get; private set; }
    [SerializeField] private ProductUpgradeUI _upgradeUI;
    [SerializeField] private ContructionUI _constructUI;
    public ProductUpgradeUI UpgradeUI => _upgradeUI;
    public ContructionUI ContructionUI => _constructUI;
    private void Awake()
    {
        Instance = this;
    }
}