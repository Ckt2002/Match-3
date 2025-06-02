using System.Collections.Generic;

public interface IListCreator
{
    List<PotionController> Create(int type, int spawnNumber);
}
