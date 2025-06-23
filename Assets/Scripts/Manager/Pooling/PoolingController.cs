using Assets.Scripts.Manager.Pooling;
using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoBehaviour
{
    public static PoolingController Instance;
    public Dictionary<int, List<PotionController>> potions { get; private set; }
    public Dictionary<int, List<SpecialController>> specials { get; private set; }
    public TileController[,] tiles { get; private set; }

    public List<LightningVFX> lightnings { get; private set; }

    public Vector2Int potionRandomRange;

    IPotionPooling potionPooling;
    ISpecialPooling specialPooling;
    ITilePooling tilePooling;
    ILightningPooling lightningPooling;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        potionPooling = GetComponent<PotionPooling>();
        tilePooling = GetComponent<TilePooling>();
        specialPooling = GetComponent<SpecialPooling>();
        lightningPooling = GetComponent<LightningPooling>();
    }

    public void AssignPotionRange(Vector2Int potionRandomRange)
    {
        this.potionRandomRange = potionRandomRange;
    }

    public void CreateGame(int width, int height)
    {
        potions = potionPooling.Create(width * height / 2);
        specials = specialPooling.Create(width * height / 2);
        lightnings = lightningPooling.Create(width * height / 2);
        tiles = tilePooling.Create(width, height);
    }

    public PotionController GetRandomPotion()
    {
        int type = Random.Range(potionRandomRange.x, potionRandomRange.y);
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

    public PotionController GetPotion(int index)
    {
        if (index < 0)
            return null;

        foreach (var potion in potions[index])
            if (!potion.gameObject.activeInHierarchy)
            {
                potion.gameObject.SetActive(true);
                return potion;
            }

        var newPotions = potionPooling.Create(index, 1);
        potions[index].AddRange(newPotions);
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

    public LightningVFX GetLightning()
    {
        foreach (var lightning in lightnings)
            if (!lightning.gameObject.activeInHierarchy)
            {
                lightning.gameObject.SetActive(true);
                return lightning;
            }

        var news = lightningPooling.Create(1);
        lightnings.AddRange(news);
        news[0].gameObject.SetActive(true);
        return news[0];
    }
}