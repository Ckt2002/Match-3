using UnityEngine;

public class DestroyPotion
{
    DestroyObstacle destroyObstacle;
    TileController[,] tiles;
    int width, height;

    public DestroyPotion(BoardGrid boardGrid)
    {
        this.tiles = boardGrid.tiles;
        this.width = boardGrid.width;
        this.height = boardGrid.height;

        destroyObstacle = new DestroyObstacle(tiles, width, height);
    }

    public void DestroyOne(int w, int h)
    {
        if (tiles[w, h].currentObstacle != null)
            destroyObstacle.DestroyOneObstacle(w, h);
        else
        {
            destroyObstacle.DestroyObstacleInLine(w, h);
            tiles[w, h].ActiveSpecial();
            tiles[w, h].HidePotion();
        }
    }

    public void DestroyOnHorizontal(int w, int h)
    {
        for (int i = 0; i < width; i++)
            DestroyOne(i, h);
    }

    public void DestroyOnVertical(int w, int h)
    {
        for (int i = 0; i < height; i++)
            DestroyOne(w, i);
    }

    public void DestroyGrid3(int w, int h)
    {
        int startW = w - 1;
        int startH = h - 1;
        int endW = w + 1;
        int endH = h + 1;
        for (int j = startH; j <= endH; j++)
            for (int i = startW; i <= endW; i++)
            {
                bool validInd = i >= 0 && i < height &&
                    j >= 0 && j < height;
                if (validInd && tiles[i, j] != null)
                    DestroyOne(i, j);
            }
    }

    public void DestroyAllByType(PotionController potion = null)
    {
        if (potion == null || potion.specialType == ESpecialType.Lightning)
        {
            int potionIndex = Random.Range(0, 4);
            EPotion potionType = (EPotion)potionIndex;
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    if (tiles[i, j] == null)
                        continue;
                    PotionController currentPotion = tiles[i, j].potion;
                    if (currentPotion != null)
                        if (currentPotion.potionType == potionType &&
                            currentPotion.specialType == ESpecialType.None)
                            DestroyOne(i, j);
                }
        }
        else
        {
            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    if (tiles[i, j] == null)
                        continue;

                    PotionController currentPotion = tiles[i, j].potion;
                    if (currentPotion != null)
                        if (currentPotion.potionType == potion.potionType &&
                            currentPotion.specialType == potion.specialType)
                            DestroyOne(i, j);
                }
        }
    }
}
