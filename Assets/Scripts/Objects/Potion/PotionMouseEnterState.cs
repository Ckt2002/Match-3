using DG.Tweening;
using UnityEngine;

public class PotionMouseEnterState : IPotionState
{
    PotionController potion;
    float scaleDuration;
    Vector3 mouseEnterScale;
    Transform potionTransform;

    public PotionMouseEnterState(PotionController potion, float scaleDuration
        , Vector3 mouseEnterScale)
    {
        this.potion = potion;
        this.scaleDuration = scaleDuration;
        this.mouseEnterScale = mouseEnterScale;
        potionTransform = potion.transform;
    }

    public void Enter()
    {
        potionTransform.DOScale(mouseEnterScale, scaleDuration);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        //potionTransform.DOKill();
        potion = null;
        potionTransform = null;
    }
}