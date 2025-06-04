public class DestroyPotion
{
    TileController[,] tiles;
    int width, height;

    public void DestroyOne(int w, int h)
    {
        tiles[w, h].HidePotion();
    }

    public void DestroyOnHorizontal(int h)
    {
        for (int w = 0; w < width; w++)
            tiles[w, h].HidePotion();
    }

    public void DestroyOnVertical(int w)
    {
        for (int h = 0; h < height; h++)
            tiles[w, h].HidePotion();
    }

    public void DestroyGrid3(int w, int h)
    {
        int startW = w - 1;
        int startH = h - 1;
        int endW = w + 1;
        int endH = h + 1;

        for (int j = startH; j <= endH; j++)
            for (int i = startW; i <= endW; i++)
                tiles[i, j].HidePotion();
    }
}
