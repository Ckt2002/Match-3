using System;

[Serializable]
public class PotionRequire
{
    public EPotion potionType;
    public int number;

    public PotionRequire(EPotion potionType, int number)
    {
        this.potionType = potionType;
        this.number = number;
    }
}
