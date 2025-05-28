using UnityEngine;

public class MoveDownStrategy : IMoveStrategy
{
    public Vector2Int GetTargetIndex(Vector2Int currentIndex) =>
        new Vector2Int(currentIndex.x, currentIndex.y - 1);
}
