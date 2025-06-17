using System;
using System.Collections;
using UnityEngine;

public class MovePotions
{
    public Func<int, int, IEnumerator> spawnFunc;
    public Func<IEnumerator> checkRegenerateFunc;
    TileController[,] tiles;
    int width, height;

    public MovePotions(BoardGrid boardGrid)
    {
        tiles = boardGrid.tiles;
        width = boardGrid.width;
        height = boardGrid.height;
    }

    public IEnumerator MoveDown(int fromW, int toW, int fromH, int toH)
    {
        bool moved = false;
        for (int w = fromW; w < toW; w++)
        {
            moved = false;
            for (int h = fromH; h < toH; h++)
            {
                TileController currentTile = tiles[w, h];
                if (currentTile.currentObstacle != null)
                    break;

                if (currentTile == null || !currentTile.gameObject.activeInHierarchy || currentTile.potion != null)
                    continue;

                for (int hTemp = h + 1; hTemp < toH; hTemp++)
                {
                    TileController targetTile = tiles[w, hTemp];
                    if (tiles[w, hTemp].currentObstacle != null)
                        break;

                    if (tiles[w, hTemp].potion == null ||
                        !tiles[w, hTemp].gameObject.activeInHierarchy)
                        continue;

                    currentTile.ChangePotion(tiles[w, hTemp].potion);
                    tiles[w, hTemp].AssignPotion(null);
                    break;
                }
                moved = true;
            }
            if (moved)
                yield return spawnFunc(w, w + 1);
        }
        yield return new WaitForSeconds(0.0002f);

        yield return checkRegenerateFunc();
    }

    public IEnumerator MoveDiagonally()
    {
        // Move left and right diagonally 
        for (int w = 0; w < width; w++)
        {
            for (int h = 1; h < height; h++)
            {
                TileController currentTile = tiles[w, h];
                if (currentTile.currentObstacle != null)
                    break;

                if (currentTile == null || !currentTile.gameObject.activeInHierarchy ||
                    currentTile.potion == null || h == 0)
                    continue;

                TileController leftTile = w - 1 >= 0 ? tiles[w - 1, h - 1] : null;
                TileController rightTile = w + 1 < width ? tiles[w + 1, h - 1] : null;

                // Check if either side is available
                bool canMoveLeft = leftTile != null && leftTile.potion == null && leftTile.currentObstacle == null;
                bool canMoveRight = rightTile != null && rightTile.potion == null && rightTile.currentObstacle == null;

                if (canMoveLeft || canMoveRight)
                {

                    for (int hTemp = 0; hTemp < h; hTemp++)
                    {
                        bool moved = false;
                        // Check and move left
                        if (canMoveLeft)
                        {
                            for (int wTemp = w - 1; wTemp >= 0; wTemp--)
                            {
                                TileController targetTile = tiles[wTemp, hTemp];
                                if (targetTile.gameObject.activeInHierarchy &&
                                    targetTile.potion == null &&
                                    targetTile.currentObstacle == null)
                                {
                                    targetTile.ChangePotion(currentTile.potion);
                                    currentTile.AssignPotion(null);
                                    moved = true;
                                    break;
                                }
                            }
                        }

                        if (moved)
                            // move down and refill
                            yield return MoveDown(w, w + 1, 0, height);

                        moved = false;
                        // Check and move right
                        if (canMoveRight)
                        {
                            for (int wTemp = w + 1; wTemp < width; wTemp++)
                            {
                                TileController targetTile = tiles[wTemp, hTemp];
                                if (targetTile.gameObject.activeInHierarchy && targetTile.potion == null &&
                                    targetTile.currentObstacle == null)
                                {
                                    targetTile.ChangePotion(currentTile.potion);
                                    currentTile.AssignPotion(null);
                                    moved = true;
                                    break;
                                }
                            }
                        }

                        if (moved)
                            // move down and refill
                            yield return MoveDown(w, w + 1, 0, height);
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.0002f);
    }
}
