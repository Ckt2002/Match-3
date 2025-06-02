using System.Collections.Generic;
using UnityEngine;

public class PotionFactory : IPotionFactory
{
    public List<PotionController> Create(GameObject potionPrefab,
        Transform potionContainer, int spawnNumber)
    {
        if (spawnNumber <= 0)
            return null;

        List<PotionController> potions = new List<PotionController>();
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject potion = GameObject.Instantiate(potionPrefab, potionContainer);
            potion.gameObject.SetActive(false);
            potions.Add(potion.GetComponent<PotionController>());
        }
        return potions;
    }
}
