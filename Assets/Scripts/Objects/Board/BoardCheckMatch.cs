using System.Collections.Generic;
using UnityEngine;

public class BoardCheckMatch
{
    GameObject[,] potionGrid;
    int width, height;
    PotionController selectedPotion, swappedPotion;
    BoardFindMatch findMatch;

    public BoardCheckMatch()
    {
    }

    public void Setup(BoardGrid boardGrid
        , PotionController selectedPotion, PotionController swappedPotion)
    {
        potionGrid = boardGrid.potionGrid;
        width = boardGrid.gridWidth;
        height = boardGrid.gridHeight;
        this.selectedPotion = selectedPotion;
        this.swappedPotion = swappedPotion;

        findMatch = new BoardFindMatch(potionGrid, width, height);
    }

    public void Match()
    {
        List<PotionController> matches = new List<PotionController>();
        matches.AddRange(findMatch.FindHorizontalMatches(
            (int)selectedPotion.potionIndex.x, (int)selectedPotion.potionIndex.y));
        matches.AddRange(findMatch.FindVerticalMatches(
            (int)selectedPotion.potionIndex.x, (int)selectedPotion.potionIndex.y));

        if (selectedPotion.potionType == swappedPotion.potionType)
        {
            matches.AddRange(findMatch.FindHorizontalMatches(
                (int)swappedPotion.potionIndex.x, (int)swappedPotion.potionIndex.y));
            matches.AddRange(findMatch.FindVerticalMatches(
                (int)swappedPotion.potionIndex.x, (int)swappedPotion.potionIndex.y));
        }
        else
        {
            MatchAction(matches);
            matches.Clear();
            matches.AddRange(findMatch.FindHorizontalMatches(
                (int)swappedPotion.potionIndex.x, (int)swappedPotion.potionIndex.y));
            matches.AddRange(findMatch.FindVerticalMatches(
                (int)swappedPotion.potionIndex.x, (int)swappedPotion.potionIndex.y));
        }
        MatchAction(matches);
        matches.Clear();
        matches = null;
    }

    private void MatchAction(List<PotionController> matches)
    {
        if (matches.Count >= 3)
        {
            Debug.Log("Match");
        }
        else
        {
            Debug.Log("Not match");
        }
    }
}
