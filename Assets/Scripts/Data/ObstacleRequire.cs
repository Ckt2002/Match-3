using System;

[Serializable]
public class ObstacleRequire
{
    public EObstacle obstacleType;
    public int number;

    public ObstacleRequire(EObstacle obstacleType, int number)
    {
        this.obstacleType = obstacleType;
        this.number = number;
    }
}
