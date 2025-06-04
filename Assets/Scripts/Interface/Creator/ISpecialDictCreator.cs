using System.Collections.Generic;

public interface ISpecialDictCreator
{
    Dictionary<int, List<SpecialController>> Create(int spawnNumber);
}