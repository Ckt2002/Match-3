using UnityEngine;

public class DestroyPotion
{
    DestroyObstacle destroyObstacle;
    PoolingController poolingController;
    TileController[,] tiles;
    int width, height;

    public DestroyPotion(BoardGrid boardGrid)
    {
        this.tiles = boardGrid.tiles;
        this.width = boardGrid.width;
        this.height = boardGrid.height;

        destroyObstacle = new DestroyObstacle(tiles, width, height);
    }

    public void DestroyOne(int w, int h, Vector3? startPos = null, Vector3? endPos = null)
    {
        if (tiles[w, h].currentObstacle != null)
            destroyObstacle.DestroyOneObstacle(w, h);
        else
        {
            destroyObstacle.DestroyObstacleInLine(w, h);
            tiles[w, h].ActiveSpecial(startPos, endPos);
            tiles[w, h].HidePotion();
        }
    }

    public void DestroyOnHorizontal(int w, int h)
    {
        Vector3 hStartPos, hEndPos;
        hStartPos = hEndPos = Vector3.zero;

        // Find from the right
        for (int i = 0; i <= w; i++)
            if (tiles[i, h].gameObject.activeInHierarchy)
            {
                hStartPos = tiles[i, h].transform.position;
                break;
            }

        // Find from the left
        for (int i = width - 1; i >= w; i--)
            if (tiles[i, h].gameObject.activeInHierarchy)
            {
                hEndPos = tiles[i, h].transform.position;
                break;
            }

        for (int i = 0; i < width; i++)
            DestroyOne(i, h, hStartPos, hEndPos);
    }

    public void DestroyOnVertical(int w, int h)
    {
        Vector3 vStartPos, vEndPos;
        vStartPos = vEndPos = Vector3.zero;

        // Find from the bottom
        for (int i = 0; i <= h; i++)
            if (tiles[w, i].gameObject.activeInHierarchy)
            {
                vStartPos = tiles[w, i].transform.position;
                break;
            }

        // Find from the top
        for (int i = height - 1; i >= h; i--)
            if (tiles[w, i].gameObject.activeInHierarchy)
            {
                vEndPos = tiles[w, i].transform.position;
                break;
            }

        for (int i = 0; i < height; i++)
            DestroyOne(w, i, vStartPos, vEndPos);
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

    public void DestroyAllByType(TileController swappedTile, TileController selectedTile)
    {
        if (poolingController == null)
            poolingController = PoolingController.Instance;

        PotionController swappedPotion = swappedTile?.potion;
        PotionController selectedPotion = selectedTile?.potion;

        if (swappedTile == null || swappedTile.potion.specialType == ESpecialType.Lightning)
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
                        {
                            DestroyOne(i, j);
                            LightningVFX lightning = poolingController.GetLightning();
                            if (swappedTile != null)
                                lightning.transform.position = swappedTile.transform.position;
                            else
                                lightning.transform.position = selectedTile.transform.position;

                            lightning.SetupVFX(tiles[i, j].transform.localPosition);
                            continue;
                        }
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
                        if (currentPotion.potionType == swappedPotion.potionType &&
                            currentPotion.specialType == swappedPotion.specialType)
                        {
                            DestroyOne(i, j);
                            LightningVFX lightning = poolingController.GetLightning();
                            if (swappedTile != null)
                                lightning.transform.position = swappedTile.transform.position;
                            else
                                lightning.transform.position = selectedTile.transform.position;

                            lightning.SetupVFX(tiles[i, j].transform.localPosition);
                            continue;
                        }
                }
        }
    }
}
