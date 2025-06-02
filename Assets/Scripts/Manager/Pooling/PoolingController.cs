using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoBehaviour
{
    public static PoolingController Instance;
    public Dictionary<int, List<PotionController>> potions { get; private set; }
    public TileController[,] tiles { get; private set; }

    IPotionPooling potionPooling;
    ITilePooling tilePooling;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        potionPooling = GetComponent<PotionPooling>();
        tilePooling = GetComponent<TilePooling>();
    }

    public void CreateGame(int width, int height)
    {
        potions = potionPooling.Create(width * height / 2);
        tiles = tilePooling.Create(width, height);
    }

    public PotionController GetRandomPotion()
    {
        int type = Random.Range(0, potions.Count);
        foreach (var potion in potions[type])
            if (!potion.gameObject.activeInHierarchy)
            {
                potion.gameObject.SetActive(true);
                return potion;
            }

        var newPotions = potionPooling.Create(type, 1);
        potions[type].AddRange(newPotions);
        newPotions[0].gameObject.SetActive(true);
        return newPotions[0];
    }
}