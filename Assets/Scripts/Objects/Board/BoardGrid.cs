public class BoardGrid
{
    public TileController[,] tiles { get; private set; }
    public int width { get; private set; }
    public int height { get; private set; }

    public void InitTile(TileController[,] tiles, int width, int height)
    {
        this.tiles = tiles;
        this.width = width;
        this.height = height;
    }
}
