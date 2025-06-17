public class BoardGrid
{
    public TileController[,] tiles { get; private set; }
    public int width { get; private set; }
    public int height { get; private set; }
    public TileController selectedTile { get; private set; }
    public TileController swappedTile { get; private set; }

    public void InitTile(TileController[,] tiles, int width, int height)
    {
        this.tiles = tiles;
        this.width = width;
        this.height = height;
    }

    public void AssignSelectedTile(TileController selectedTile)
    {
        this.selectedTile = selectedTile;
    }

    public void AssignSwappedTile(TileController swappedTile)
    {
        this.swappedTile = swappedTile;
    }

    public void ResetTileTemp()
    {
        AssignSelectedTile(null);
        AssignSwappedTile(null);
    }
}
