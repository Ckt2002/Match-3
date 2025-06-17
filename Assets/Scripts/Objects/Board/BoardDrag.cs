using System;
using UnityEngine;

public class BoardDrag
{
    public Action<Vector2Int, Vector2Int> swapAction;
    BoardGrid boardGrid;

    public BoardDrag(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
    }

    public void Drag(EMoveType moveType)
    {
        if (boardGrid.selectedTile == null)
            return;

        Vector2Int nexTileIndex = boardGrid.selectedTile.tileIndex;
        Vector2Int selectedTileIndex = boardGrid.selectedTile.tileIndex;

        IMoveStrategy strategy = moveType switch
        {
            EMoveType.Up => new MoveUpStrategy(),
            EMoveType.Down => new MoveDownStrategy(),
            EMoveType.Left => new MoveLeftStrategy(),
            EMoveType.Right => new MoveRightStrategy(),
            _ => null
        };

        if (strategy == null || boardGrid.selectedTile == null)
            return;

        swapAction.Invoke(strategy.GetTargetIndex(nexTileIndex), selectedTileIndex);
    }
}
