using UnityEngine;

public interface IArranger
{
    void Arrange(int width, int height, float spacing,
        Vector3 spawnPosOffset, BoardController boardController, PoolingController poolingController);
}
