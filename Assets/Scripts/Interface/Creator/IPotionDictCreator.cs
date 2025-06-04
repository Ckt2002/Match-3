using System.Collections.Generic;

public interface IPotionDictCreator
{
    Dictionary<int, List<PotionController>> Create(int spawnNumber);
}