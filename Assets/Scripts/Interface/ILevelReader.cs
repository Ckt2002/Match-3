using System.Threading.Tasks;

public interface ILevelReader
{
    Task<LevelData> ReadLevelData(int levelToLoad);
}
