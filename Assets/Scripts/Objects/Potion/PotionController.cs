using DG.Tweening;
using System;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public EPotion potionType;
    public Vector3 zoomsize = Vector3.one;
    public float scaleDuration = 0.3f;
    public float moveDuration = 0.3f;
    protected Vector3 originalSize;

    protected void Awake()
    {
        originalSize = transform.localScale;
    }

    public void Hide(Action action = null)
    {
        transform.DOScale(Vector3.zero, scaleDuration).OnComplete(() =>
        {
            action?.Invoke();
            gameObject.SetActive(false);
        });
    }

    public void ZoomScale()
    {
        ChangeScale(zoomsize);
    }

    public void ResetScale()
    {
        ChangeScale(originalSize);
    }

    public void ChangeScale(Vector3 scale)
    {
        transform.DOScale(scale, scaleDuration);
    }

    public void Move(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration);
    }

    public virtual void ActiveSpecial()
    {
    }
}
