using UnityEngine;

public interface IMoveStrategy
{
    Vector2Int GetTargetIndex(Vector2Int currentIndex);
}
