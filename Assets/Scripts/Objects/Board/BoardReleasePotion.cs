using System;

public class BoardReleasePotion
{
    Action undoAction;
    BoardCheckMatch boardCheckMatch;

    public BoardReleasePotion(Action action)
    {
        undoAction += action;
    }

    public void Setup(BoardGrid boardGrid
        , PotionController selectedPotion, PotionController swappedPotion)
    {
        if (boardCheckMatch == null)
            boardCheckMatch = new BoardCheckMatch();

        boardCheckMatch.Setup(boardGrid, selectedPotion, swappedPotion);
    }

    public void Release()
    {
        boardCheckMatch.Match();
        undoAction.Invoke();
    }
}