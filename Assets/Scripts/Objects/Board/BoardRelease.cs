using UnityEngine;

public class BoardRelease
{
    BoardCheckMatch boardCheckMatch;
    MonoBehaviour coroutineLauncher;

    public BoardRelease(BoardCheckMatch boardCheckMatch,
        MonoBehaviour coroutineLauncher)
    {
        this.boardCheckMatch = boardCheckMatch;
        this.coroutineLauncher = coroutineLauncher;
    }

    public void Release()
    {
        coroutineLauncher.StartCoroutine(boardCheckMatch.CheckMatchAfterSwap());
    }
}