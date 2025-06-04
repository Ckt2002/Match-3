using System.Collections.Generic;

public interface ISpecialListCreator
{
    List<SpecialController> Create(int type, int spawnNumber);
}
