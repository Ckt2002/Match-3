using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T>
{
    List<T> Create(GameObject prefab, Transform potionContainer, int spawnNumber);
}
