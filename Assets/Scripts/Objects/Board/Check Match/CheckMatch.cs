using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatch
{
    public ProcessMatch processMatch;

    private readonly BoardGrid boardGrid;
    private readonly FindMatches boardFindMatches;

    public CheckMatch(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
        this.boardFindMatches = new FindMatches(boardGrid);
        this.processMatch = new ProcessMatch(boardGrid);
    }

    public IEnumerator CheckAllMatches()
    {
        GameController.mouseInteract = false;
        HashSet<TileController> matches;
        do
        {
            matches = FindAllMatches();
            yield return processMatch.ProcessMatches(matches);
            yield return new WaitForSeconds(0.5f);
        }
        while (matches.Count > 0);

        GameController.mouseInteract = true;
    }

    public IEnumerator CheckMatchAfterSwap()
    {
        TileController selectedTile = boardGrid.selectedTile;
        TileController swappedTile = boardGrid.swappedTile;

        PotionController selectedPotion = boardGrid.selectedTile.potion;
        PotionController swappedPotion = boardGrid.swappedTile.potion;

        bool tileNull = selectedTile == null || swappedTile == null;
        bool potionNull = selectedPotion == null || swappedPotion == null;
        if (tileNull || potionNull)
            yield break;

        yield return Check();
        yield return CheckAllMatches();
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator Check()
    {
        GameController.mouseInteract = false;
        if (boardGrid.selectedTile == null || boardGrid.swappedTile == null)
            yield break;

        var matches = new HashSet<TileController>();
        AddMatchesForTile(boardGrid.selectedTile.tileIndex, matches);
        AddMatchesForTile(boardGrid.swappedTile.tileIndex, matches);

        yield return processMatch.ProcessMatches(matches);
        yield return new WaitForSeconds(0.5f);
    }

    private HashSet<TileController> FindAllMatches()
    {
        var allMatches = new HashSet<TileController>();
        for (int x = 0; x < boardGrid.width; x++)
            for (int y = 0; y < boardGrid.height; y++)
                AddMatchesForTile(new Vector2Int(x, y), allMatches);

        return allMatches;
    }

    private void AddMatchesForTile(Vector2Int tileIndex, HashSet<TileController> matches)
    {
        matches.UnionWith(boardFindMatches.FindHorizontalMatches(tileIndex.x, tileIndex.y));
        matches.UnionWith(boardFindMatches.FindVerticalMatches(tileIndex.x, tileIndex.y));
    }
}