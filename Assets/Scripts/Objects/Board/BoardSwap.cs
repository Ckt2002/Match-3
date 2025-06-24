using System;
using System.Collections;
using UnityEngine;

public class BoardSwap
{
    public Func<IEnumerator> checkMatchFunc;

    BoardGrid boardGrid;
    TileController[,] tiles;
    MonoBehaviour coroutineLauncher;
    int gridWidth, gridHeight;

    public BoardSwap(BoardGrid boardGrid, MonoBehaviour coroutineLauncher)
    {
        this.boardGrid = boardGrid;
        this.coroutineLauncher = coroutineLauncher;
    }

    public void InitSwap()
    {
        tiles = boardGrid.tiles;
        gridWidth = boardGrid.width;
        gridHeight = boardGrid.height;
    }

    public void Swap(Vector2Int swappedTileIndex, Vector2Int selectedTileIndex)
    {
        if (tiles == null)
            InitSwap();

        if (boardGrid.swappedTile == null)
            boardGrid.AssignSwappedTile(GetSwappedTile(swappedTileIndex));

        if (boardGrid.swappedTile != null && boardGrid.selectedTile != null)
        {
            PotionController swappedPotion = tiles[swappedTileIndex.x, swappedTileIndex.y].potion;

            tiles[swappedTileIndex.x, swappedTileIndex.y].ChangePotion(boardGrid.selectedTile.potion);
            tiles[selectedTileIndex.x, selectedTileIndex.y].ChangePotion(swappedPotion);
        }

        coroutineLauncher.StartCoroutine(checkMatchFunc());
    }

    private TileController GetSwappedTile(Vector2Int swappedPotionIndex)
    {
        bool validTile = tiles[swappedPotionIndex.x, swappedPotionIndex.y].gameObject.activeInHierarchy;
        bool widthValid = swappedPotionIndex.x >= 0 && swappedPotionIndex.x < gridWidth;
        bool heightValid = swappedPotionIndex.y >= 0 && swappedPotionIndex.y < gridHeight;

        if (validTile && widthValid && heightValid)
            return tiles[swappedPotionIndex.x, swappedPotionIndex.y];
        else
            return null;
    }

    public void Undo()
    {
        if (boardGrid.selectedTile != null && boardGrid.swappedTile != null)
            Swap(boardGrid.swappedTile.tileIndex, boardGrid.selectedTile.tileIndex);
        boardGrid.ResetTileTemp();
    }
}
