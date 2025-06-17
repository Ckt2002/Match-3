public class DestroyObstacle
{
    TileController[,] tiles;
    int width, height;

    public DestroyObstacle(TileController[,] tiles, int width, int height)
    {
        this.tiles = tiles;
        this.width = width;
        this.height = height;
    }

    public void DestroyObstacleInLine(int w, int h)
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

                bool validLine = i == w || j == h;

                if (validInd && validLine && tiles[i, j].currentObstacle != null)
                    DestroyOneObstacle(i, j);
            }
    }

    public void DestroyObstacleAroundPotion(int w, int h)
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

                if (validInd && tiles[i, j].currentObstacle != null)
                    DestroyOneObstacle(i, j);
            }
    }

    public void DestroyOneObstacle(int w, int h)
    {
        tiles[w, h].DestroyCurrentObstacle();
    }
}
