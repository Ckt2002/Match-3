using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    public BoardGrid boardGrid { get; private set; }
    public BoardSwap boardSwap { get; private set; }
    public BoardDrag boardDrag { get; private set; }
    public BoardRelease boardRelease { get; private set; }
    public BoardRefill boardRefill { get; private set; }
    public BoardCheckMatch boardCheckMatch { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        boardGrid = new BoardGrid();
        boardSwap = new BoardSwap(boardGrid);
        boardDrag = new BoardDrag(boardSwap.Swap);
        boardRefill = new BoardRefill(boardGrid, this);
        boardCheckMatch = new BoardCheckMatch(boardGrid, boardSwap, boardRefill, this);
        boardRelease = new BoardRelease(boardCheckMatch, this);
    }

    public void SetSelectedTile(TileController tile)
    {
        boardDrag.SetSelectedTile(tile);
        boardSwap.SetSelectedPotion(tile);
    }

    public void DragTile(EMoveType moveType)
    {
        boardDrag.Drag(moveType);
    }

    public void ReleaseTile()
    {
        if (boardSwap.selectedTile == null)
            return;

        boardRelease.Release();
        boardDrag.Reset();
    }

    //public void
}