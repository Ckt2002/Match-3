using System.Collections.Generic;
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

    public async void Arrange(int width, int height)
    {
        LevelData level = await LevelController.Instance.LoadLevel();
        List<TileData> tileData = level.Tiles;

        BoardGrid boardGrid = BoardController.Instance.boardGrid;
        PoolingController poolingController = PoolingController.Instance;
        poolingController.AssignPotionRange(level.PotionRange);

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
            }
        }

        foreach (var tile in tileData)
        {
            int w = tile.W;
            int h = tile.H;
            tiles[w, h].gameObject.SetActive(tile.Active);
            tiles[w, h].AssignObstacle(tile.ObstacleIndex);
            tiles[w, h].AssignPotion(PoolingController.Instance.GetPotion(tile.PotionIndex));
        }

        boardGrid.InitTile(tiles, width, height);
        CheckMatch checkMatch = BoardController.Instance.boardCheckMatch;
        StartCoroutine(checkMatch.CheckAllMatches());
    }
}
