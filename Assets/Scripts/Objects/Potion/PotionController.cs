using DG.Tweening;
using System;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public EPotionType potionType;
    public float scaleDuration = 0.3f;
    public float moveDuration = 0.3f;

    public void Hide(Action action = null)
    {
        transform.DOScale(Vector3.zero, scaleDuration).OnComplete(() =>
        {
            action?.Invoke();
            gameObject.SetActive(false);
        });
    }

    public void ChangeScale(Vector3 scale)
    {
        transform.DOScale(scale, scaleDuration);
    }

    public void Move(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration);
    }
}
