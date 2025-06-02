using System.Collections.Generic;
using UnityEngine;

public class PotionPooling : MonoBehaviour, IPotionPooling
{
    public GameObject[] potionPrefabs;
    public Transform potionContainer;
    IPotionFactory potionFactory;

    private void Awake()
    {
        potionFactory = new PotionFactory();
    }

    public Dictionary<int, List<PotionController>> Create(int spawnNumber)
    {
        Dictionary<int, List<PotionController>> potionDict = new();
        for (int i = 0; i < potionPrefabs.Length; i++)
        {
            potionDict.TryAdd(i,
                potionFactory.Create(potionPrefabs[i], potionContainer, spawnNumber / 2));
        }

        return potionDict;
    }

    public List<PotionController> Create(int type, int spawnNumber)
    {
        return potionFactory.Create(potionPrefabs[type], potionContainer, spawnNumber);
    }
}
