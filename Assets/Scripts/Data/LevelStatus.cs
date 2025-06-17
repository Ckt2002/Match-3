using System;

[Serializable]
public class LevelStatus
{
    public int Level;
    public bool Locked;

    public LevelStatus(int level, bool locked = true)
    {
        Level = level;
        Locked = locked;
    }
}
