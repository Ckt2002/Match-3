using UnityEngine;

public class MoveLeftStrategy : IMoveStrategy
{
    public Vector2Int GetTargetIndex(Vector2Int currentIndex) =>
        new Vector2Int(currentIndex.x - 1, currentIndex.y);
}
