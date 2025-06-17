using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class FileReader : IFileReader
{
    public async Task<LevelData> ReadLevelData(int levelToLoad)
    {
        string levelPath = $"Levels/Level_{levelToLoad}";
        TextAsset levelAsset = Resources.Load<TextAsset>(levelPath);
        if (levelAsset == null)
            throw new FileNotFoundException($"Level file not found at Resources/{levelPath}");

        string jsonContent = levelAsset.text;

        // Optional: Simulate async work
        await Task.Yield();

        LevelData levelData = await Task.Run(() => JsonUtility.FromJson<LevelData>(jsonContent));
        return levelData;
    }

    public async Task<LevelManager> ReadLevelManager()
    {

        string levelPath = $"Levels/LevelManager";
        TextAsset levelAsset = Resources.Load<TextAsset>(levelPath);
        if (levelAsset == null)
            throw new FileNotFoundException($"Level file not found at Resources/{levelPath}");

        string jsonContent = levelAsset.text;

        // Optional: Simulate async work
        await Task.Yield();

        LevelManager levelManager = await Task.Run(() => JsonUtility.FromJson<LevelManager>(jsonContent));
        return levelManager;
    }
}
