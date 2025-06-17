using System;
using System.Collections;
using UnityEngine;

public class CheckRegenerateBoard
{
    BoardGrid board;
    TileController[,] tiles;
    int width, height;
    CheckPossibleMoves possibleMoves;
    public Func<int, int, IEnumerator> refillFunc;
    public Func<IEnumerator> checkAllMatchFunc;

    public CheckRegenerateBoard(BoardGrid board)
    {
        this.board = board;
    }

    public IEnumerator Regenerate()
    {
        if (tiles == null)
        {
            tiles = board.tiles;
            width = board.width;
            height = board.height;
        }

        if (possibleMoves == null)
            possibleMoves = new CheckPossibleMoves();
        bool checkPossibleMoves = false;

        yield return possibleMoves.HasPossibleMoves(tiles, width, height, (a) => checkPossibleMoves = a);

        if (checkPossibleMoves)
        {
            yield break;
        }

        Debug.Log("There are no more possible moves");
        yield return new WaitForSeconds(1f);

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                TileController currentTile = tiles[w, h];
                if (!currentTile.gameObject.activeInHierarchy)
                    continue;

                currentTile.ClearTilePotion();
            }
        }

        yield return new WaitForSeconds(0.3f);
        yield return refillFunc(0, width);
        yield return checkAllMatchFunc();
    }
}
