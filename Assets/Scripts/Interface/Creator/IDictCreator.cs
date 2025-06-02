using System.Collections.Generic;

public interface IDictCreator
{
    Dictionary<int, List<PotionController>> Create(int spawnNumber);
}