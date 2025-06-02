using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCheckMatch
{
    BoardGrid boardGrid;
    BoardSwap swapPotion;
    BoardRefill boardRefill;
    BoardFindMatches boardFindMatches;
    MonoBehaviour coroutineLauncher;

    public BoardCheckMatch(BoardGrid boardGrid, BoardSwap swapPotion,
        BoardRefill boardRefill, MonoBehaviour coroutineLauncher)
    {
        this.boardGrid = boardGrid;
        this.swapPotion = swapPotion;
        this.boardRefill = boardRefill;
        this.coroutineLauncher = coroutineLauncher;
        boardFindMatches = new BoardFindMatches(boardGrid);
    }

    public IEnumerator CheckAllMatches()
    {
        TileController[,] tiles = boardGrid.tiles;
        int height = boardGrid.height;
        int width = boardGrid.width;
        HashSet<TileController> uniqueMatches = new HashSet<TileController>();

        do
        {
            uniqueMatches.Clear();
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                    yield return coroutineLauncher.StartCoroutine(
                        GetMatchesFound(new Vector2Int(w, h), uniqueMatches)
                        );

            yield return MatchAction(uniqueMatches);
        }
        while (uniqueMatches.Count > 0);

        yield return null;
    }

    public IEnumerator CheckMatch()
    {
        if (swapPotion.selectedTile == null || swapPotion.swappedTile == null)
            yield break;

        Vector2Int selectedTileInd = swapPotion.selectedTile.tileIndex;
        Vector2Int swappedTileInd = swapPotion.swappedTile.tileIndex;

        HashSet<TileController> uniqueMatches = new HashSet<TileController>();

        yield return coroutineLauncher.StartCoroutine(
            GetMatchesFound(selectedTileInd, uniqueMatches)
            );

        yield return coroutineLauncher.StartCoroutine(
            GetMatchesFound(swappedTileInd, uniqueMatches)
            );

        yield return coroutineLauncher.StartCoroutine(MatchAction(uniqueMatches));

        yield return coroutineLauncher.StartCoroutine(CheckAllMatches());
    }

    private IEnumerator GetMatchesFound(Vector2Int tileIndex, HashSet<TileController> uniqueMatches)
    {
        uniqueMatches.UnionWith(boardFindMatches.FindHorizontalMatches(
            tileIndex.x, tileIndex.y));
        uniqueMatches.UnionWith(boardFindMatches.FindVerticalMatches(
            tileIndex.x, tileIndex.y));

        yield return null;
    }

    private IEnumerator MatchAction(HashSet<TileController> potionsMatch)
    {
        TileController[,] tiles = boardGrid.tiles;

        if (potionsMatch.Count >= 3)
        {
            foreach (var potion in potionsMatch)
            {
                tiles[potion.tileIndex.x, potion.tileIndex.y].HidePotion();
            }

            yield return new WaitForSeconds(0.3f);
            yield return coroutineLauncher.StartCoroutine(boardRefill.Refill());
            swapPotion.Reset();
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            swapPotion.Undo();
        }
    }
}