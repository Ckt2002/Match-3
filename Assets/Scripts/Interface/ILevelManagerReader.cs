using System.Threading.Tasks;

public interface ILevelManagerReader
{
    Task<LevelManager> ReadLevelManager();
}
