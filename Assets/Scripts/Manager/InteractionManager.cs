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

        if (Physics.Raycast(ray, out hit, _maxRaycastDst))
        {
            IClickable clickable = hit.collider.GetComponent<IClickable>();

            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }
}