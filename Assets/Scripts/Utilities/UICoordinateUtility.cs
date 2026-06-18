using UnityEngine;

public static class UICoordinateUtility
{
    public static Vector2 WorldToOverlayCanvasPosition(Vector3 worldPosition, Camera mainCamera, RectTransform parentCanvasRect)
    {
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasRect, screenPoint, null, out Vector2 localPoint);
        return localPoint;
    }
}