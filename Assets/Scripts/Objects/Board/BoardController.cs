using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    public BoardGrid boardGrid { get; private set; }
    public BoardSwap boardSwap { get; private set; }
    public BoardDrag boardDrag { get; private set; }
    public BoardRefill boardRefill { get; private set; }
    public CheckMatch boardCheckMatch { get; private set; }
    public DestroyPotion destroyPotion { get; private set; }
    public CheckRegenerateBoard checkRegenerateBoard { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        boardGrid = new BoardGrid();
        boardDrag = new BoardDrag(boardGrid);
        boardSwap = new BoardSwap(boardGrid, this);
        boardRefill = new BoardRefill(boardGrid);
        boardCheckMatch = new CheckMatch(boardGrid);
        checkRegenerateBoard = new CheckRegenerateBoard(boardGrid);

        boardDrag.swapAction += boardSwap.Swap;
        boardSwap.checkMatchFunc = boardCheckMatch.CheckMatchAfterSwap;

        checkRegenerateBoard.checkAllMatchFunc = boardCheckMatch.CheckAllMatches;
        checkRegenerateBoard.refillFunc = boardRefill.FillRemainNull;
        boardRefill.checkRegenerateFunc = checkRegenerateBoard.Regenerate;

        boardCheckMatch.processMatch.refillFunc = boardRefill.Refill;
        boardCheckMatch.processMatch.undoAction += boardSwap.Undo;
        boardCheckMatch.processMatch.destroyTileAction += DestroyPotion;
        boardCheckMatch.processMatch.resetTileTempAction += boardGrid.ResetTileTemp;
    }

    public void SetSelectedTile(TileController tile)
    {
        boardGrid.AssignSelectedTile(tile);
    }

    public void DragTile(EMoveType moveType)
    {
        boardDrag.Drag(moveType);
    }

    public void DestroyPotion(TileController tile, PotionController potion)
    {
        if (destroyPotion == null)
            destroyPotion = new DestroyPotion(boardGrid, this);

        int w = tile.tileIndex.x;
        int h = tile.tileIndex.y;

        ESpecialType specialType = potion.specialType;

        switch (specialType)
        {
            case ESpecialType.None:
                destroyPotion.DestroyOne(w, h);
                break;

            case ESpecialType.Explosion:
                destroyPotion.DestroyByBomb(w, h);
                break;

            case ESpecialType.H:
                destroyPotion.DestroyOnHorizontal(w, h);
                break;

            case ESpecialType.V:
                destroyPotion.DestroyOnVertical(w, h);
                break;

            case ESpecialType.Lightning:

                TileController swappedTile = boardGrid.swappedTile;
                TileController selectedTile = boardGrid.selectedTile;
                destroyPotion.DestroyAllByLightning(swappedTile, selectedTile, tile);
                break;

            default:
                Debug.LogWarning("Special type does not exist.");
                break;
        }
    }
}