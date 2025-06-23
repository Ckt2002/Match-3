using DG.Tweening;
using System;
using UnityEngine;

public class PotionController : MonoBehaviour, IPotion
{
    public EPotion potionType;
    public ESpecialType specialType;
    public Vector3 zoomsize = Vector3.one;
    public float scaleDuration = 0.3f;
    public float moveDuration = 0.3f;
    protected Vector3 originalSize;
    public bool isSpecial = false;

    [SerializeField] protected GameObject VFX;

    protected void Awake()
    {
        originalSize = transform.localScale;
    }

    public void Hide(Action action = null)
    {
        if (VFX != null)
            VFX.GetComponent<ParticleSystem>()?.Play();
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

    public virtual void ActiveSpecial(TileController tile,
        Vector3? startPos = null, Vector3? endPos = null)
    {
        if (specialType != ESpecialType.None)
        {
            BoardController.Instance.DestroyPotion(tile, this);
            if (specialType == ESpecialType.H ||
                specialType == ESpecialType.V)
            {
                Vector3 start = startPos ?? tile.transform.position;
                Vector3 end = endPos ?? tile.transform.position;

                if (startPos.HasValue && endPos.HasValue)
                {
                    var swipeVFX = VFX?.GetComponent<SwipeVFX>();
                    if (swipeVFX != null)
                    {
                        swipeVFX.SetupVFX(start, end);
                        swipeVFX.RunVFX();
                    }
                }
            }
            return;
        }
    }
}
