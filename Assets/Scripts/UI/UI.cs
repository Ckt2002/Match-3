using DG.Tweening;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] protected EUI UIType;

    public void HandleUIChanged(EUI type)
    {
        if (type == UIType)
            ShowUI();
        else
            HideUI();
    }

    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
        // Run animation
    }

    protected virtual void ShowUIOnComplete()
    {
    }

    public virtual void HideByType(EUI uiType)
    {
        if (this.UIType == uiType)
            HideUI();
    }

    public virtual void HideUI()
    {
        // Run animation
        gameObject.SetActive(false);
    }

    protected virtual void HideUIOnComplete()
    {
    }

    protected void OnDestroy()
    {
        DOTween.KillAll(true);
    }
}
