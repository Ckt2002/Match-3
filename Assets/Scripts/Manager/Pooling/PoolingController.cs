using Assets.Scripts.Manager.Pooling;
using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoBehaviour
{
    public static PoolingController Instance;
    public Dictionary<int, List<PotionController>> potions { get; private set; }
    public Dictionary<int, List<SpecialController>> specials { get; private set; }
    public TileController[,] tiles { get; private set; }

    IPotionPooling potionPooling;
    ISpecialPooling specialPooling;
    ITilePooling tilePooling;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        potionPooling = GetComponent<PotionPooling>();
        tilePooling = GetComponent<TilePooling>();
        specialPooling = GetComponent<SpecialPooling>();
    }

    public void CreateGame(int width, int height)
    {
        potions = potionPooling.Create(width * height / 2);
        specials = specialPooling.Create(width * height / 2);
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

    public SpecialController GetSpecial(int type)
    {
        foreach (var special in specials[type])
            if (!special.gameObject.activeInHierarchy)
            {
                special.gameObject.SetActive(true);
                return special;
            }

        var newSpecials = specialPooling.Create(type, 1);
        specials[type].AddRange(newSpecials);
        newSpecials[0].gameObject.SetActive(true);
        return newSpecials[0];
    }
}