using DG.Tweening;
using UnityEngine;

internal class PotionMouseExitState : IPotionState
{
    PotionController potion;
    float scaleDuration;
    Vector3 originalScale;
    Transform potionTransform;

    public PotionMouseExitState(PotionController potion, float scaleDuration, Vector3 originalScale)
    {
        this.potion = potion;
        this.scaleDuration = scaleDuration;
        this.originalScale = originalScale;
        potionTransform = potion.transform;
    }

    public void Enter()
    {
        potionTransform.DOScale(originalScale, scaleDuration);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        potionTransform = null;
    }
}