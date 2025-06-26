using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Action<ECanvasGroup, EUI> OnOpenCanvas;
    public Action<ECanvasGroup, EUI> OnCloseCanvas;
    public Transform UIContainer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (Transform child in UIContainer)
        {
            if (child.GetComponent<CanvasUI>() == null)
                return;

            CanvasUI canvas = child.GetComponent<CanvasUI>();
            OnOpenCanvas += canvas.HandleCanvasChange;
            OnCloseCanvas += canvas.HandleCanvasHide;
        }
    }

    public void OpenUI(ECanvasGroup canvasGroup, EUI uiType)
    {
        OnOpenCanvas.Invoke(canvasGroup, uiType);
    }

    public void CloseUI(ECanvasGroup canvasGroup, EUI uiType)
    {
        OnCloseCanvas.Invoke(canvasGroup, uiType);
    }

    public void CloseAll()
    {
    }
}