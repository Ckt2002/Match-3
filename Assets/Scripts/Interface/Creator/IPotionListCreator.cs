using System.Collections.Generic;

public interface IPotionListCreator
{
    List<PotionController> Create(int type, int spawnNumber);
}
