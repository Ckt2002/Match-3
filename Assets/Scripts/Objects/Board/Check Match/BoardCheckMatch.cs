using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCheckMatch
{
    private readonly BoardGrid boardGrid;
    private readonly BoardSwap swapPotion;
    private readonly BoardRefill boardRefill;
    private readonly BoardFindMatches boardFindMatches;
    private readonly MonoBehaviour coroutineLauncher;

    public BoardCheckMatch(BoardGrid boardGrid, BoardSwap swapPotion,
        BoardRefill boardRefill, MonoBehaviour coroutineLauncher)
    {
        this.boardGrid = boardGrid;
        this.swapPotion = swapPotion;
        this.boardRefill = boardRefill;
        this.coroutineLauncher = coroutineLauncher;
        this.boardFindMatches = new BoardFindMatches(boardGrid);
    }

    public IEnumerator CheckAllMatches()
    {
        HashSet<TileController> matches;
        do
        {
            matches = FindAllMatches();
            yield return coroutineLauncher.StartCoroutine(ProcessMatches(matches));
        }
        while (matches.Count > 0);
    }

    public IEnumerator CheckMatchAfterSwap()
    {
        if (swapPotion.selectedTile == null || swapPotion.swappedTile == null)
            yield break;

        var matches = new HashSet<TileController>();
        AddMatchesForTile(swapPotion.selectedTile.tileIndex, matches);
        AddMatchesForTile(swapPotion.swappedTile.tileIndex, matches);

        yield return coroutineLauncher.StartCoroutine(ProcessMatches(matches));
        yield return coroutineLauncher.StartCoroutine(CheckAllMatches());
    }

    private HashSet<TileController> FindAllMatches()
    {
        var allMatches = new HashSet<TileController>();
        for (int x = 0; x < boardGrid.width; x++)
        {
            for (int y = 0; y < boardGrid.height; y++)
            {
                AddMatchesForTile(new Vector2Int(x, y), allMatches);
            }
        }
        return allMatches;
    }

    private void AddMatchesForTile(Vector2Int tileIndex, HashSet<TileController> matches)
    {
        matches.UnionWith(boardFindMatches.FindHorizontalMatches(tileIndex.x, tileIndex.y));
        matches.UnionWith(boardFindMatches.FindVerticalMatches(tileIndex.x, tileIndex.y));
    }

    private IEnumerator ProcessMatches(HashSet<TileController> matches)
    {
        yield return new WaitForSeconds(0.3f);
        if (matches.Count < 3)
        {
            yield return coroutineLauncher.StartCoroutine(HandleFailedSwap());
            yield break;
        }

        var specialBlocks = FindSpecialBlock.FindSpecialBlocks(matches);

        if (specialBlocks.Count > 0)
            matches.ExceptWith(specialBlocks.Keys);

        yield return coroutineLauncher.StartCoroutine(ClearTiles(matches));
        yield return new WaitForSeconds(0.3f);
        yield return coroutineLauncher.StartCoroutine(boardRefill.Refill());
        yield return new WaitForSeconds(0.3f);
        swapPotion.Reset();
    }

    private IEnumerator ClearTiles(HashSet<TileController> tiles)
    {
        foreach (var tile in tiles)
        {
            tile.HidePotion();
        }
        yield return new WaitForSeconds(0.3f);
    }

    private IEnumerator HandleFailedSwap()
    {
        yield return new WaitForSeconds(0.1f);
        swapPotion.Undo();
    }
}