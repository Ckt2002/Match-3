using System;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    public ECanvasGroup canvasGroup;

    Action<EUI> OnOpenUI;
    Action<EUI> OnHideUI;
    Action OnCloseAll;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<UI>() == null)
                continue;

            UI ui = child.GetComponent<UI>();
            OnOpenUI += ui.HandleUIChanged;
            OnHideUI += ui.HideByType;
            OnCloseAll += ui.HideUI;
        }
    }

    public void HandleCanvasChange(ECanvasGroup canvasGroup, EUI uiType)
    {
        if (this.canvasGroup == canvasGroup)
        {
            gameObject.SetActive(true);
            OnOpenUI.Invoke(uiType);
        }
        else
        {
            HandleCloseUI();
            gameObject.SetActive(false);
        }
    }

    public void HandleCanvasHide(ECanvasGroup canvasGroup, EUI uiType)
    {
        if (this.canvasGroup == canvasGroup)
        {
            OnHideUI.Invoke(uiType);
            gameObject.SetActive(false);
        }
    }

    public void HandleCloseUI()
    {
        OnCloseAll.Invoke();
    }
}