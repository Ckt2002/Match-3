using UnityEngine;

public class BoardGrid
{
    public GameObject[,] potionGrid { get; private set; }
    public GameObject[,] tileGrid { get; private set; }
    public int gridWidth { get; private set; }
    public int gridHeight { get; private set; }

    public void InitTileGrid(int width, int height)
    {
        tileGrid = new GameObject[width, height];
    }

    public void InitPotionGrid(int width, int height)
    {
        potionGrid = new GameObject[width, height];
    }

    public void InitGridSize(int width, int height)
    {
        gridWidth = width;
        gridHeight = height;
    }

    public void SetTileGrid(GameObject tile, int widthInd, int heightInd)
    {
        tileGrid[widthInd, heightInd] = tile;
    }

    public void SetPotionGrid(GameObject potion, int widthInd, int heightInd)
    {
        potionGrid[widthInd, heightInd] = potion;
        potionGrid[widthInd, heightInd]
            .GetComponent<PotionController>()
            .SetPotionIndex(new Vector2Int(widthInd, heightInd));
    }
}
