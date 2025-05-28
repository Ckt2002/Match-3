using System;
using UnityEngine;

public class BoardDragPotion
{
    Action<Vector2Int, Vector2Int> swapAction;
    PotionController selectedPotion;

    public BoardDragPotion(Action<Vector2Int, Vector2Int> action)
    {
        swapAction += action;
    }

    public void SetSelectedPotion(PotionController selectedPotion)
    {
        this.selectedPotion = selectedPotion;
    }

    public void Drag(EMoveType moveType)
    {

        Vector2Int nextToPotionIndex = selectedPotion.potionIndex;
        Vector2Int selectedPotionIndex = selectedPotion.potionIndex;

        IMoveStrategy strategy = moveType switch
        {
            EMoveType.Up => new MoveUpStrategy(),
            EMoveType.Down => new MoveDownStrategy(),
            EMoveType.Left => new MoveLeftStrategy(),
            EMoveType.Right => new MoveRightStrategy(),
            _ => null
        };

        if (strategy == null || selectedPotion == null)
            return;

        swapAction.Invoke(strategy.GetTargetIndex(nextToPotionIndex), selectedPotionIndex);
    }
}
