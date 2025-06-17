using System.Threading.Tasks;
using UnityEngine;

public class FileReadController : MonoBehaviour
{
    public static FileReadController Instance { get; private set; }

    private IFileReader levelReader;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        levelReader = new FileReader();
    }

    public async Task<LevelData> ReadLevelFile(int level)
    {
        LevelData data = await levelReader.ReadLevelData(level);
        return data;
    }

    public async Task<LevelManager> ReadLevelManagerFile()
    {
        LevelManager data = await levelReader.ReadLevelManager();
        return data;
    }
}
