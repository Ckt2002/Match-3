using System;
using System.Collections;
using System.Collections.Generic;

public class BoardRefill
{
    BoardGrid boardGrid;
    PoolingController poolingController;
    MovePotions movePotions;
    TileController[,] tiles;
    int width, height;
    public Func<IEnumerator> checkRegenerateFunc;

    public BoardRefill(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
    }

    public IEnumerator InitRefill()
    {
        tiles = boardGrid.tiles;
        width = boardGrid.width;
        height = boardGrid.height;
        poolingController = PoolingController.Instance;
        movePotions = new MovePotions(boardGrid);
        movePotions.spawnFunc = FillRemainNull;
        movePotions.checkRegenerateFunc = checkRegenerateFunc;

        yield return null;
    }

    public IEnumerator Refill()
    {
        if (tiles == null)
            yield return InitRefill();

        yield return MovePotion();
    }

    public IEnumerator MovePotion()
    {
        yield return movePotions.MoveDown(0, width, 0, height);

        yield return movePotions.MoveDiagonally();
    }

    public IEnumerator FillRemainNull(int fromW, int toW)
    {
        if (tiles == null)
            yield return InitRefill();

        List<TileController> tilesTemp = new();
        for (int w = fromW; w < toW; w++)
        {
            tilesTemp.Clear();
            for (int h = height - 1; h >= 0; h--)
            {
                if (tiles[w, h].currentObstacle != null)
                    break;

                if (!tiles[w, h].gameObject.activeInHierarchy || tiles[w, h].potion != null)
                    continue;
                tilesTemp.Add(tiles[w, h]);
            }
            tilesTemp.Reverse();
            yield return null;
            foreach (var tile in tilesTemp)
            {
                PotionController potion = poolingController.GetRandomPotion();
                potion.transform.localPosition = tiles[w, height - 1].transform.localPosition;
                tile.ChangePotion(potion);
            }
            yield return null;
        }
    }
}