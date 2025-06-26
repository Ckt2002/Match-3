using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int Level;
    public int Score;
    public Vector2Int PotionRange;
    public List<PotionRequire> PotionRequires;
    public List<ObstacleRequire> ObstacleRequires;
    public List<TileData> Tiles;

    public LevelData(int level, Vector2Int potionRange, int score = 0)
    {
        this.Level = level;
        this.Score = score;
        this.PotionRange = potionRange;
        Tiles = new();
        PotionRequires = new();
        ObstacleRequires = new();
    }
}
