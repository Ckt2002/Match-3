using System.Collections.Generic;

public class BoardFindMatches
{
    BoardGrid boardGrid;
    TileController[,] tiles;
    int width, height;

    public BoardFindMatches(BoardGrid boardGrid)
    {
        this.boardGrid = boardGrid;
    }

    private void Init()
    {
        if (tiles == null)
        {
            tiles = boardGrid.tiles;
            width = boardGrid.width;
            height = boardGrid.height;
        }
    }

    public List<TileController> FindHorizontalMatches(int w, int h)
    {
        Init();

        List<TileController> matches = new List<TileController>();
        matches.Add(tiles[w, h]);

        // Left
        for (int i = w - 1; i >= 0; i--)
        {
            if (tiles[i, h].potion.potionType ==
                tiles[w, h].potion.potionType)
                matches.Add(tiles[i, h]);
            else
                break;
        }

        // Right
        for (int i = w + 1; i < width; i++)
        {
            if (tiles[i, h].potion.potionType ==
                tiles[w, h].potion.potionType)
                matches.Add(tiles[i, h]);
            else
                break;
        }

        return matches.Count >= 3 ? matches : new List<TileController>();
    }

    public List<TileController> FindVerticalMatches(int w, int h)
    {
        Init();

        List<TileController> matches = new List<TileController>();
        matches.Add(tiles[w, h]);

        // Down
        for (int i = h - 1; i >= 0; i--)
        {
            if (tiles[w, i].potion.potionType ==
                tiles[w, h].potion.potionType)
                matches.Add(tiles[w, i]);
            else
                break;
        }

        // Up
        for (int i = h + 1; i < height; i++)
        {
            if (tiles[w, i].potion.potionType ==
                tiles[w, h].potion.potionType)
                matches.Add(tiles[w, i]);
            else
                break;
        }

        return matches.Count >= 3 ? matches : new List<TileController>();
    }
}
