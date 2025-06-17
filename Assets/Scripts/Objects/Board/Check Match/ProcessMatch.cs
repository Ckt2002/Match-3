using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMatch
{
    public Action undoAction;
    public Action resetTileTempAction;
    public Action<TileController, PotionController> destroyTileAction;
    public Func<IEnumerator> refillFunc;

    BoardGrid boardGrid;

    public ProcessMatch(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
    }

    public IEnumerator ProcessMatches(HashSet<TileController> matches)
    {
        yield return new WaitForSeconds(0.3f);

        TileController selectedTile = boardGrid.selectedTile;
        TileController swappedTile = boardGrid.swappedTile;

        bool tilesNull = selectedTile == null && swappedTile == null;
        bool bothSpecial = !tilesNull && selectedTile.potion.isSpecial && swappedTile.potion.isSpecial;
        bool selectedLightning = !tilesNull && selectedTile.potion.specialType == ESpecialType.Lightning;
        bool swappedLightning = !tilesNull && swappedTile.potion.specialType == ESpecialType.Lightning;
        bool oneLightning = selectedLightning || swappedLightning;

        if (bothSpecial)
        {
            selectedTile.ActiveSpecial();
            swappedTile.ActiveSpecial();

            yield return ClearTiles(matches);
        }
        else if (oneLightning)
        {
            PotionController selectedPotion = boardGrid.selectedTile.potion;
            PotionController swappedPotion = boardGrid.swappedTile.potion;
            if (selectedLightning)
                selectedTile.ActiveSpecial();
            else
                swappedTile.ActiveSpecial();
        }
        else
        {
            if (matches.Count < 3)
            {
                yield return HandleFailedSwap();
                GameController.mouseInteract = true;
                yield break;
            }

            var specialBlocks = FindSpecialBlock.FindSpecialBlocks(matches, boardGrid);

            yield return ClearTiles(matches, specialBlocks);

        }
        yield return new WaitForSeconds(0.5f);
        yield return refillFunc();
    }

    public IEnumerator ClearTiles(HashSet<TileController> tiles = null,
        Dictionary<TileController, ESpecialType> specials = null)
    {
        if (tiles != null)
            foreach (var tile in tiles)
                destroyTileAction.Invoke(tile, tile.potion);

        if (specials != null)
            foreach (var special in specials)
                special.Key.AssignSpecialType(special.Value);

        yield return new WaitForSeconds(0.3f);
        resetTileTempAction.Invoke();
    }

    private IEnumerator HandleFailedSwap()
    {
        yield return new WaitForSeconds(0.1f);
        undoAction.Invoke();
    }
}