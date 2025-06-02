using System.Collections;
using UnityEngine;

public class BoardRefill
{
    BoardGrid boardGrid;
    MonoBehaviour coroutineLauncher;
    PoolingController poolingController;
    TileController[,] tiles;
    int width, height;

    public BoardRefill(BoardGrid boardGrid, MonoBehaviour coroutineLauncher)
    {
        this.boardGrid = boardGrid;
        this.coroutineLauncher = coroutineLauncher;
    }

    public IEnumerator InitRefill()
    {
        tiles = boardGrid.tiles;
        poolingController = PoolingController.Instance;
        width = boardGrid.width;
        height = boardGrid.height;

        yield return null;
    }

    public IEnumerator Refill()
    {
        if (tiles == null)
            yield return coroutineLauncher.StartCoroutine(InitRefill());

        yield return coroutineLauncher.StartCoroutine(MoveDown());

        yield return coroutineLauncher.StartCoroutine(FillRemainNull());
    }

    public IEnumerator MoveDown()
    {
        bool completed = false;

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (tiles[w, h].potion == null)
                {
                    for (int ny = h + 1; ny < height; ny++)
                    {
                        if (tiles[w, ny].potion != null)
                        {
                            tiles[w, h].ChangePotion(tiles[w, ny].potion);
                            tiles[w, ny].SetPotion(null);
                            break;
                        }
                    }
                }
            }
        }
        completed = true;
        yield return new WaitUntil(() => completed);
    }

    public IEnumerator FillRemainNull()
    {
        bool completed = false;
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (tiles[w, h].potion == null)
                {
                    PotionController potion = poolingController.GetRandomPotion();
                    potion.transform.localPosition = tiles[w, height - 1].transform.localPosition;
                    tiles[w, h].ChangePotion(potion);
                }
            }
        }
        completed = true;
        yield return new WaitUntil(() => completed);
    }
}