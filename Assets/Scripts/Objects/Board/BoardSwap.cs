using UnityEngine;

public class BoardSwap
{
    BoardGrid boardGrid;
    TileController[,] tiles;
    int gridWidth, gridHeight;

    public TileController selectedTile { get; private set; }
    public TileController swappedTile { get; private set; }

    public BoardSwap(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
    }

    public void InitSwap()
    {
        tiles = boardGrid.tiles;
        gridWidth = boardGrid.width;
        gridHeight = boardGrid.height;
    }

    public void SetSelectedPotion(TileController selectedTile)
    {
        this.selectedTile = selectedTile;
    }

    public void Swap(Vector2Int swappedTileIndex, Vector2Int selectedTileIndex)
    {
        if (tiles == null)
            InitSwap();

        if (swappedTile == null)
            swappedTile = GetSwappedTile(swappedTileIndex);

        if (swappedTile != null && selectedTile != null)
        {
            PotionController swappedPotion = tiles[swappedTileIndex.x, swappedTileIndex.y].potion;

            tiles[swappedTileIndex.x, swappedTileIndex.y].ChangePotion(selectedTile.potion);
            tiles[selectedTileIndex.x, selectedTileIndex.y].ChangePotion(swappedPotion);
        }
        else
            Debug.LogWarning("The next to potion does not exist");
    }

    private TileController GetSwappedTile(Vector2Int swappedPotionIndex)
    {
        bool widthValid = swappedPotionIndex.x >= 0 && swappedPotionIndex.x < gridWidth;
        bool heightValid = swappedPotionIndex.y >= 0 && swappedPotionIndex.y < gridHeight;

        if (widthValid && heightValid)
            return tiles[swappedPotionIndex.x, swappedPotionIndex.y];
        else
            return null;
    }

    public void Undo()
    {
        if (selectedTile != null && swappedTile != null)
            Swap(swappedTile.tileIndex, selectedTile.tileIndex);
        Reset();
    }

    public void Reset()
    {
        selectedTile = null;
        swappedTile = null;
    }
}
