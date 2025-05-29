using DG.Tweening;
using System;
using UnityEngine;

public class PotionHideState : IPotionState
{
    PotionController controller;
    Action resetAction;

    public PotionHideState(PotionController controller, Action reset)
    {
        this.controller = controller;
        resetAction = reset;
    }

    public void Enter()
    {
        controller.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            resetAction.Invoke();
            controller.gameObject.SetActive(false);
            Exit();
        });
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        controller = null;
    }
}