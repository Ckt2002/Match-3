using System.Collections.Generic;
using UnityEngine;

public class SpecialFactory : ISpecialFactory
{
    public List<SpecialController> Create(GameObject potionPrefab,
        Transform potionContainer, int spawnNumber)
    {
        if (spawnNumber <= 0)
            return null;

        List<SpecialController> potions = new List<SpecialController>();
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject potion = GameObject.Instantiate(potionPrefab, potionContainer);
            potion.gameObject.SetActive(false);
            potions.Add(potion.GetComponent<SpecialController>());
        }
        return potions;
    }
}