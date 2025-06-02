using System;
using UnityEngine;

public class BoardDrag
{
    Action<Vector2Int, Vector2Int> swapAction;
    TileController selectedTile;

    public BoardDrag(Action<Vector2Int, Vector2Int> action)
    {
        swapAction += action;
    }

    public void SetSelectedTile(TileController selectedTile)
    {
        this.selectedTile = selectedTile;
    }

    public void Drag(EMoveType moveType)
    {
        Vector2Int nexTileIndex = selectedTile.tileIndex;
        Vector2Int selectedTileIndex = selectedTile.tileIndex;

        IMoveStrategy strategy = moveType switch
        {
            EMoveType.Up => new MoveUpStrategy(),
            EMoveType.Down => new MoveDownStrategy(),
            EMoveType.Left => new MoveLeftStrategy(),
            EMoveType.Right => new MoveRightStrategy(),
            _ => null
        };

        if (strategy == null || selectedTile == null)
            return;

        swapAction.Invoke(strategy.GetTargetIndex(nexTileIndex), selectedTileIndex);
    }

    public void Reset()
    {
        selectedTile = null;
    }
}
