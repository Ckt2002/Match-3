using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    public BoardGrid boardGrid { get; private set; }
    public BoardSwapPotion boardSwapPotion { get; private set; }
    public BoardDragPotion boardDragPotion { get; private set; }
    public BoardReleasePotion boardReleasePotion { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        boardGrid = new BoardGrid();
        boardSwapPotion = new BoardSwapPotion(boardGrid);
        boardDragPotion = new BoardDragPotion(boardSwapPotion.Swap);
        boardReleasePotion = new BoardReleasePotion(boardSwapPotion.Undo);
    }

    public void SetSelectedPotion(PotionController potion)
    {
        boardDragPotion.SetSelectedPotion(potion);
        boardSwapPotion.SetSelectedPotion(potion);
    }

    public void DragPotion(EMoveType moveType)
    {
        boardDragPotion.Drag(moveType);
    }

    public void ReleasePotion()
    {
        if (boardSwapPotion.selectedPotion == null)
            return;

        boardReleasePotion.Setup(boardGrid,
            boardSwapPotion.selectedPotion, boardSwapPotion.nextToPotion);
        boardReleasePotion.Release();
    }
}