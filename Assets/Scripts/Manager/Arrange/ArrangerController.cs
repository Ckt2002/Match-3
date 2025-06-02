using UnityEngine;

public class ArrangerController : MonoBehaviour
{
    public static ArrangerController Instance;
    public float spacing = 1;
    public Vector3 spawnPosOffset = Vector3.zero;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void Arrange(int width, int height)
    {
        BoardGrid boardGrid = BoardController.Instance.boardGrid;
        PoolingController poolingController = PoolingController.Instance;

        TileController[,] tiles = poolingController.tiles;

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                Vector3 tilePos = new Vector3(
                    w * spacing + spawnPosOffset.x
                    , h * spacing + spawnPosOffset.y
                    , 0f);
                TileController tile = tiles[w, h];
                tile.SetTileIndex(new Vector2Int(w, h));
                tile.transform.position = tilePos;
                tile.SetPotion(poolingController.GetRandomPotion());
            }
        }

        boardGrid.InitTile(tiles, width, height);
        BoardCheckMatch checkMatch = BoardController.Instance.boardCheckMatch;
        StartCoroutine(checkMatch.CheckAllMatches());
    }
}
