using UnityEngine;

public class BoardSwapPotion
{
    BoardGrid boardGrid;
    GameObject[,] tileGrid, potionGrid;
    int gridWidth, gridHeight;

    public PotionController selectedPotion { get; private set; }
    public PotionController nextToPotion { get; private set; }

    public BoardSwapPotion(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
        InitSwap();
    }

    public void InitSwap()
    {
        tileGrid = boardGrid.tileGrid;
        potionGrid = boardGrid.potionGrid;
        gridWidth = boardGrid.gridWidth;
        gridHeight = boardGrid.gridHeight;
    }

    public void SetSelectedPotion(PotionController selectedPotion)
    {
        this.selectedPotion = selectedPotion;
    }

    public void Swap(Vector2Int nextToPotionIndex, Vector2Int selectedPotionIndex)
    {
        if (tileGrid == null || potionGrid == null)
            InitSwap();

        if (nextToPotion == null)
            nextToPotion = GetNextToPotion(nextToPotionIndex);

        if (nextToPotion != null && selectedPotion != null)
        {
            Vector2Int indexTemp = nextToPotion.potionIndex;
            potionGrid[indexTemp.x, indexTemp.y] = selectedPotion.gameObject;
            potionGrid[selectedPotionIndex.x, selectedPotionIndex.y] = nextToPotion.gameObject;
            nextToPotion.SetPotionIndex(selectedPotionIndex);
            selectedPotion.SetPotionIndex(indexTemp);

            Vector3 nextToPotionPos = tileGrid[nextToPotionIndex.x, nextToPotionIndex.y]
                .transform.position;
            Vector3 selectedPotionPos = tileGrid[selectedPotionIndex.x, selectedPotionIndex.y]
                .transform.position;

            selectedPotion.Move(nextToPotionPos);
            nextToPotion.Move(selectedPotionPos);
        }
        else
            Debug.Log("The next to potion does not exist");
    }

    private PotionController GetNextToPotion(Vector2Int nextToPotionIndex)
    {
        bool widthValid = nextToPotionIndex.x >= 0 && nextToPotionIndex.x < gridWidth;
        bool heightValid = nextToPotionIndex.y >= 0 && nextToPotionIndex.y < gridHeight;

        if (widthValid && heightValid)
            return potionGrid[nextToPotionIndex.x, nextToPotionIndex.y]
                .GetComponent<PotionController>();
        else
            return null;
    }

    public void Undo()
    {
        if (selectedPotion != null && nextToPotion != null)
            Swap(nextToPotion.potionIndex, selectedPotion.potionIndex);
        Reset();
    }

    public void Reset()
    {
        selectedPotion = null;
        nextToPotion = null;
    }
}
