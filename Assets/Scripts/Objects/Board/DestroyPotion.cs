using UnityEngine;

public class DestroyPotion
{
    DestroyObstacle destroyObstacle;
    DestroyPotionByLightning destroyPotionByLightning;
    MonoBehaviour coroutineLauncher;
    TileController[,] tiles;
    int width, height;

    public DestroyPotion(BoardGrid boardGrid, MonoBehaviour coroutineLauncher)
    {
        this.tiles = boardGrid.tiles;
        this.width = boardGrid.width;
        this.height = boardGrid.height;
        this.coroutineLauncher = coroutineLauncher;
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

    public void DestroyByBomb(int w, int h)
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

    public void DestroyAllByLightning(TileController swappedTile,
        TileController selectedTile,
        TileController currentTile)
    {
        if (swappedTile == null || selectedTile == null || currentTile == null)
            return;

        if (destroyPotionByLightning == null)
        {
            destroyPotionByLightning = new DestroyPotionByLightning(tiles, width, height);
            destroyPotionByLightning.destroyOneAct += DestroyOne;
        }

        PotionController swappedPotion = swappedTile?.potion;
        PotionController selectedPotion = selectedTile?.potion;

        bool liSwap = currentTile == swappedTile || currentTile == selectedTile;
        bool liAndNor = swappedPotion.specialType == ESpecialType.None ||
            selectedPotion.specialType == ESpecialType.None;
        bool liAndSwipeH = swappedPotion.specialType == ESpecialType.H ||
            selectedPotion.specialType == ESpecialType.H;
        bool liAndSwipeV = swappedPotion.specialType == ESpecialType.V ||
            selectedPotion.specialType == ESpecialType.V;
        bool liAndBomb = swappedPotion.specialType == ESpecialType.Explosion ||
            selectedPotion.specialType == ESpecialType.Explosion;
        bool liAndLi = swappedPotion.specialType == ESpecialType.Lightning &&
            selectedPotion.specialType == ESpecialType.Lightning;

        // Check if lightning is not swapped or selected potion (means it has been actived by orther specials)
        if (!liSwap || liAndBomb)
        {
            // destroy random
            destroyPotionByLightning.ComboRandom(currentTile);
            currentTile.HidePotion();
            return;
        }

        // If current lightning swapped with any potion
        // Check if swapped with normal
        if (liAndNor)
        {
            // call li and nor combo
            PotionController normalPotion = swappedPotion;
            if (selectedPotion.specialType == ESpecialType.None)
                normalPotion = selectedPotion;
            destroyPotionByLightning.ComboLiNor(currentTile, normalPotion);
            currentTile.HidePotion();
            return;
        }

        // Check if swapped with swipe special
        if (liAndSwipeH || liAndSwipeV)
        {
            // call li and swipe combo
            EPotion potionType = swappedPotion.potionType;
            if (potionType == EPotion.None)
                potionType = selectedPotion.potionType;

            coroutineLauncher.StartCoroutine(
                destroyPotionByLightning.ComboLiSw(currentTile, potionType));
            return;
        }

        // Check if both are lightning
        if (liAndLi)
        {
            destroyPotionByLightning.ComboLiLi();
            return;
        }
    }
}
