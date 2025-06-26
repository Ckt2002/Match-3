#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelTool : MonoBehaviour
{
    public GameObject tilePrefab;
    public int height, width;
    public Vector3 spawnPosOffset = Vector3.zero;
    public float spacing;
    public int level;
    public int maxLevel;
    public List<PotionRequire> potionRequires;
    public List<ObstacleRequire> obstacleRequires;
    public TileController[,] tiles;
    public Transform tilesParent;
    public Transform potionsParent;
    public Vector2Int randomPotionRange;
    public GameObject[] potionsPrefab;

    [ContextMenu("Create Tiles")]
    public void CreateTiles()
    {
        tiles = new TileController[height, width];
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                Vector3 tilePos = new Vector3(
                    w * spacing + spawnPosOffset.x
                    , h * spacing + spawnPosOffset.y
                    , 0f);
                GameObject go = Instantiate(tilePrefab, tilesParent);
                TileController tile = go.GetComponent<TileController>();
                tile.SetTileIndex(new Vector2Int(w, h));
                tile.transform.position = tilePos;
                tiles[w, h] = tile;
            }
        }
    }

    [ContextMenu("Save Level")]
    public void SaveLevel()
    {
        LevelData levelData = new LevelData(level, randomPotionRange);

        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                int specialIndex = -1;
                bool isActive = tiles[w, h].gameObject.activeInHierarchy;
                if (tiles[w, h].currentObstacle != null)
                    specialIndex = Array.FindIndex(tiles[w, h].obstacles, ob => ob == tiles[w, h].currentObstacle);

                if (tiles[w, h].potion == null)
                    levelData.Tiles.Add(new TileData(w, h, isActive, specialIndex));
                else
                {
                    var potionType = tiles[w, h].potion.potionType;
                    levelData.Tiles.Add(new TileData(w, h, isActive, specialIndex, (int)potionType));
                }
            }
        }

        levelData.PotionRequires = potionRequires;
        levelData.ObstacleRequires = obstacleRequires;

        string json = JsonUtility.ToJson(levelData, true);
        string folderPath = Path.Combine(Application.dataPath, "Resources/Levels");
        string filePath = Path.Combine(folderPath, $"Level_{level}.json");

        // Ensure directory exists
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Save file
        File.WriteAllText(filePath, json);
        Debug.Log($"Level {level} saved to: {filePath}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

    [ContextMenu("Clear Tiles")]
    public void ClearTiles()
    {
        if (tiles == null || tiles.Length == 0)
        {
            tiles = new TileController[width, height];
            var children = new List<GameObject>();
            foreach (Transform child in tilesParent.transform)
                children.Add(child.gameObject);

            foreach (var child in children)
            {
                TileController tile = child.GetComponent<TileController>();
                tiles[tile.tileIndex.x, tile.tileIndex.y] = tile;
            }
        }

        foreach (var tile in tiles)
            tile.ClearTileTool();
    }

    [ContextMenu("Clear Level")]
    public void ClearLevel()
    {
        var children = new List<GameObject>();
        foreach (Transform child in tilesParent.transform)
            children.Add(child.gameObject);

        foreach (var child in children)
            DestroyImmediate(child);

        GameObject potions = GameObject.Find("Potions");
        var childrenPotion = new List<GameObject>();
        foreach (Transform child in potions.transform)
            childrenPotion.Add(child.gameObject);

        foreach (var child in childrenPotion)
            DestroyImmediate(child);
    }

    [ContextMenu("Assign Random Potion")]
    public void AssignRandomPotion()
    {
        var children = new List<TileController>();
        foreach (Transform child in tilesParent.transform)
            children.Add(child.GetComponent<TileController>());

        foreach (var potion in children)
        {
            if (potion.gameObject.activeInHierarchy)
            {
                int index = UnityEngine.Random.Range(randomPotionRange.x, randomPotionRange.y);
                GameObject go = Instantiate(potionsPrefab[index], potionsParent);
                potion.AssignPotionTool(go.GetComponent<PotionController>());
            }
        }
    }

    [ContextMenu("Create Level Manager")]
    public void CreateLevelManager()
    {
        LevelManager levelManager = new LevelManager();
        LevelStatus[] Levels = new LevelStatus[maxLevel];
        for (int i = 0; i < maxLevel; i++)
        {
            bool locked = i != 0;
            Levels[i] = new LevelStatus(i + 1, locked);
        }

        levelManager.Levels = Levels;

        string json = JsonUtility.ToJson(levelManager, true);
        string folderPath = Path.Combine(Application.dataPath, "Resources/LevelManager");
        string filePath = Path.Combine(folderPath, $"LevelManager.json");

        // Ensure directory exists
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Save file
        Debug.Log(json);
        Debug.Log(levelManager.Levels.Length);
        File.WriteAllText(filePath, json);
        Debug.Log($"Level Manager saved to: {filePath}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh(); // Update Unity's asset database
#endif
    }

    [ContextMenu("Unlock All Level Manager")]
    public void UnlockAllLevelManager()
    {
        LevelManager levelManager = new LevelManager();
        LevelStatus[] Levels = new LevelStatus[maxLevel];
        for (int i = 0; i < maxLevel; i++)
        {
            Levels[i] = new LevelStatus(i + 1, false);
        }

        levelManager.Levels = Levels;

        string json = JsonUtility.ToJson(levelManager, true);
        string folderPath = Path.Combine(Application.dataPath, "Resources/LevelManager");
        string filePath = Path.Combine(folderPath, $"LevelManager.json");

        // Ensure directory exists
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Save file
        Debug.Log(json);
        Debug.Log(levelManager.Levels.Length);
        File.WriteAllText(filePath, json);
        Debug.Log($"Level Manager saved to: {filePath}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh(); // Update Unity's asset database
#endif
    }
}
#endif