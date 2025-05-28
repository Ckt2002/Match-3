using DG.Tweening;
using UnityEngine;

public class PotionMoveState : IPotionState
{
    PotionController controller;
    Vector3 targetPos;

    public PotionMoveState(PotionController potionController, Vector3 targetPos)
    {
        this.controller = potionController;
        this.targetPos = targetPos;
    }

    public void Enter()
    {
        controller.transform.DOMove(targetPos, 0.3f);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        controller = null;
    }
}
