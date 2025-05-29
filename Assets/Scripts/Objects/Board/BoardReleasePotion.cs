using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardReleasePotion
{
    GameObject[,] potionGrid;
    Action undoAction;
    BoardCheckMatch boardCheckMatch;
    BoardRefill boardRefill;
    PotionController selectedPotion, swappedPotion;
    List<PotionController> matches;

    public BoardReleasePotion(Action action)
    {
        undoAction += action;
    }

    public void Setup(BoardGrid boardGrid
        , PotionController selectedPotion
        , PotionController swappedPotion
        , BoardRefill boardRefill)
    {
        this.selectedPotion = selectedPotion;
        this.swappedPotion = swappedPotion;
        potionGrid = boardGrid.potionGrid;
        this.boardRefill = boardRefill;

        if (boardCheckMatch == null)
            boardCheckMatch = new BoardCheckMatch();

        boardCheckMatch.Setup(boardGrid);
    }

    public void Release()
    {
        Match();
        selectedPotion = swappedPotion = null;
    }

    public void Match()
    {
        matches = new List<PotionController>();
        matches.AddRange(boardCheckMatch.FindHorizontalMatches(
            selectedPotion.potionIndex.x, selectedPotion.potionIndex.y));
        matches.AddRange(boardCheckMatch.FindVerticalMatches(
            selectedPotion.potionIndex.x, selectedPotion.potionIndex.y));
        matches.AddRange(boardCheckMatch.FindHorizontalMatches(
            swappedPotion.potionIndex.x, swappedPotion.potionIndex.y));
        matches.AddRange(boardCheckMatch.FindVerticalMatches(
            swappedPotion.potionIndex.x, swappedPotion.potionIndex.y));

        HashSet<PotionController> uniqueMatches;
        uniqueMatches = new HashSet<PotionController>(matches);
        MatchAction(uniqueMatches);
        matches.Clear();
    }

    private void MatchAction(HashSet<PotionController> potionsMatch)
    {
        if (potionsMatch.Count >= 3)
        {
            foreach (var potion in potionsMatch)
            {
                potionGrid[potion.potionIndex.x, potion.potionIndex.y] = null;
                potion.Hide();
            }
            boardRefill.MoveDown();
        }
        else
            undoAction.Invoke();
    }
}