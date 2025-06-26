using UnityEngine;

public class ConvertPosController : MonoBehaviour
{
    public static ConvertPosController Instance { get; private set; }
    public RectTransform canvasRectTransform;
    public Canvas canvas;                     // Your canvas component (needed for camera)

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Vector2 Transfer(Vector3 position)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null,
            out localPoint);

        return localPoint;
    }
}
