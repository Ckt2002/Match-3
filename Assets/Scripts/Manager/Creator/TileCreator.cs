using UnityEngine;

public class TileCreator : MonoBehaviour, ICreator
{
    [SerializeField] private Transform tileContainer;
    [SerializeField] private GameObject tilePrefab;

    public void Create(int width, int height, float spacing, Vector3 spawnPosOffset, BoardController boardController)
    {
        BoardGrid boardGrid = boardController.boardGrid;
        boardGrid.InitTileGrid(width, height);

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                Vector3 tilePos = new Vector3(
                    w * spacing + spawnPosOffset.x
                    , h * spacing + spawnPosOffset.y
                    , 0f);
                GameObject tile = Instantiate(tilePrefab, tileContainer);
                tile.transform.position = tilePos;

                boardGrid.SetTileGrid(tile, w, h);
            }
        }
    }
}
