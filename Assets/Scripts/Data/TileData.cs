using System;

[Serializable]
public class TileData
{
    public int W, H;
    public bool Active;
    public int PotionIndex;
    public int ObstacleIndex;

    public TileData(int w, int h, bool active = true, int obstacleIndex = -1, int potionIndex = -1)
    {
        this.W = w;
        this.H = h;
        this.Active = active;
        this.PotionIndex = potionIndex;
        this.ObstacleIndex = obstacleIndex;
    }
}
