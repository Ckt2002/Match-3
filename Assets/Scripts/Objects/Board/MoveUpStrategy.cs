using UnityEngine;

public class MoveUpStrategy : IMoveStrategy
{
    public Vector2Int GetTargetIndex(Vector2Int currentIndex) =>
        new Vector2Int(currentIndex.x, currentIndex.y + 1);
}