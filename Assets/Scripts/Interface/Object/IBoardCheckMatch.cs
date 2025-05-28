using System.Collections.Generic;
using UnityEngine;

public interface IBoardCheckMatch
{
    List<GameObject> CheckHorizontalMatches(int x, int y);
}
