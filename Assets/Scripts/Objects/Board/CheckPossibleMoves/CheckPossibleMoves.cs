using System;
using System.Collections;

public class CheckPossibleMoves
{
    public IEnumerator HasPossibleMoves(TileController[,] tiles, int width, int height, Action<bool> action)
    {
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (!tiles[w, h].gameObject.activeInHierarchy ||
                    tiles[w, h].potion == null ||
                    tiles[w, h].currentObstacle != null)
                    continue;

                // Swap Right
                if (w < width - 1)
                {
                    Swap(tiles, w, h, w + 1, h);
                    if (IsMatchAt(tiles, w, h) || IsMatchAt(tiles, w + 1, h))
                    {
                        Swap(tiles, w, h, w + 1, h); // Revert
                        action.Invoke(true);
                        yield break;
                        //return true;
                    }
                    Swap(tiles, w, h, w + 1, h); // Revert
                }

                // Swap Up
                if (h < height - 1)
                {
                    Swap(tiles, w, h, w, h + 1);
                    if (IsMatchAt(tiles, w, h) || IsMatchAt(tiles, w, h + 1))
                    {
                        Swap(tiles, w, h, w, h + 1); // Revert
                        action.Invoke(true);
                        yield break;
                        //return true;
                    }
                    Swap(tiles, w, h, w, h + 1); // Revert
                }
            }
        }

        action.Invoke(false);
        //return false;
    }

    void Swap(TileController[,] tiles, int w1, int h1, int w2, int h2)
    {
        PotionController potionTemp = tiles[w1, h1].potion;
        tiles[w1, h1].AssignPotion(tiles[w2, h2].potion, false);
        tiles[w2, h2].AssignPotion(potionTemp, false);
    }

    bool IsMatchAt(TileController[,] tiles, int w, int h)
    {
        if (!tiles[w, h].gameObject.activeInHierarchy ||
             tiles[w, h].potion == null ||
             tiles[w, h].currentObstacle != null)
            return false;

        EPotion type = tiles[w, h].potion.potionType;

        // Check horizontal
        int count = 1;
        for (int wTemp = w - 1; wTemp >= 0; wTemp--)
        {
            if (!tiles[wTemp, h].gameObject.activeInHierarchy ||
                tiles[wTemp, h].potion == null ||
                tiles[wTemp, h].currentObstacle != null ||
                tiles[wTemp, h].potion.potionType != type)
                break;
            count++;
        }

        for (int wTemp = w + 1; wTemp < tiles.GetLength(0); wTemp++)
        {
            if (!tiles[wTemp, h].gameObject.activeInHierarchy ||
                tiles[wTemp, h].potion == null ||
                tiles[wTemp, h].currentObstacle != null ||
                tiles[wTemp, h].potion.potionType != type)
                break;
            count++;
        }


        if (count >= 3)
            return true;

        // Check vertical
        count = 1;
        for (int hTemp = h - 1; hTemp >= 0; hTemp--)
        {
            if (!tiles[w, hTemp].gameObject.activeInHierarchy ||
                tiles[w, hTemp].potion == null ||
                tiles[w, hTemp].currentObstacle != null ||
                tiles[w, hTemp].potion.potionType != type)
                break;
            count++;
        }

        for (int hTemp = h + 1; hTemp < tiles.GetLength(1); hTemp++)
        {
            if (!tiles[w, hTemp].gameObject.activeInHierarchy ||
                tiles[w, hTemp].potion == null ||
                tiles[w, hTemp].currentObstacle != null ||
                tiles[w, hTemp].potion.potionType != type)
                break;
            count++;
        }


        if (count >= 3)
            return true;
        return false;
    }
}
