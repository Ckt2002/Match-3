using UnityEngine;

public interface ICreator
{
    void Create(int width, int height, float spacing, Vector3 spawnPosOffset, BoardController boardController);
}
