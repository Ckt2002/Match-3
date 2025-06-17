using System.Threading.Tasks;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public int level;

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
    }

    public async Task<LevelData> LoadLevel()
    {
        return await FileReadController.Instance.ReadLevelFile(level);
    }

    public async Task<LevelManager> LoadLevelManager()
    {
        return await FileReadController.Instance.ReadLevelManagerFile();
    }
}