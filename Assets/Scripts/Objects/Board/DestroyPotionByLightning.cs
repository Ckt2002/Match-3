using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPotionByLightning
{
    public Action<int, int, Vector3?, Vector3?> destroyOneAct;
    PoolingController poolingController;
    TileController[,] tiles;
    int width, height;

    public DestroyPotionByLightning(TileController[,] tiles, int width, int height)
    {
        this.tiles = tiles;
        this.width = width;
        this.height = height;
        poolingController = PoolingController.Instance;
    }

    public void ComboLiLi()
    {
        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++)
            {
                if (tiles[i, j] == null ||
                    !tiles[i, j].gameObject.activeInHierarchy)
                    continue;
                if (tiles[i, j].currentObstacle != null)
                {
                    tiles[i, j].currentObstacle.TakeDamage();
                    continue;
                }

                tiles[i, j].ActiveSpecial();
                tiles[i, j].HidePotion();
            }
    }

    public IEnumerator ComboLiSw(TileController currentTile, EPotion potionType)
    {
        List<TileController> tilesToDestroy = new();
        // Assign special first
        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++)
            {
                if (tiles[i, j] == null ||
                    !tiles[i, j].gameObject.activeInHierarchy ||
                    tiles[i, j].currentObstacle != null)
                    continue;
                PotionController currentPotion = tiles[i, j].potion;
                if (currentPotion.potionType == potionType && currentPotion.specialType == ESpecialType.None)
                {
                    LightningVFX lightning = poolingController.GetLightning();
                    lightning.transform.position = currentTile.transform.position;

                    lightning.SetupVFX(tiles[i, j].transform.localPosition);
                    lightning.RunVFX();
                    int specialIndex = UnityEngine.Random.Range(1, 3);
                    tiles[i, j].AssignSpecialType((ESpecialType)specialIndex);
                    tilesToDestroy.Add(tiles[i, j]);
                }
                else if (currentPotion.potionType == potionType)
                    tilesToDestroy.Add(tiles[i, j]);
            }

        yield return new WaitForSeconds(0.5f);

        // Then active them
        foreach (var tile in tilesToDestroy)
        {
            tile.ActiveSpecial();
            tile.HidePotion();
        }
        currentTile.HidePotion();
    }

    public void ComboLiNor(TileController currentTile, PotionController normalPotion)
    {
        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++)
            {
                if (tiles[i, j] == null ||
                    !tiles[i, j].gameObject.activeInHierarchy ||
                    tiles[i, j].currentObstacle != null)
                    continue;
                PotionController currentPotion = tiles[i, j].potion;
                if (currentPotion != null)
                    if (currentPotion.potionType == normalPotion.potionType &&
                        currentPotion.specialType == normalPotion.specialType)
                    {
                        destroyOneAct.Invoke(i, j, null, null);
                        LightningVFX lightning = poolingController.GetLightning();
                        lightning.transform.position = currentTile.transform.position;

                        lightning.SetupVFX(tiles[i, j].transform.localPosition);
                        lightning.RunVFX();
                        continue;
                    }
            }
    }

    public void ComboRandom(TileController currentTile)
    {
        int potionIndex = UnityEngine.Random.Range(0, 4);
        EPotion potionType = (EPotion)potionIndex;
        for (int j = 0; j < height; j++)
            for (int i = 0; i < width; i++)
            {
                if (tiles[i, j] == null ||
                    !tiles[i, j].gameObject.activeInHierarchy ||
                    tiles[i, j].currentObstacle != null)
                    continue;
                PotionController currentPotion = tiles[i, j].potion;
                if (currentPotion != null)
                    if (currentPotion.potionType == potionType &&
                        currentPotion.specialType == ESpecialType.None)
                    {
                        destroyOneAct.Invoke(i, j, null, null);
                        LightningVFX lightning = poolingController.GetLightning();
                        lightning.transform.position = currentTile.transform.position;

                        lightning.SetupVFX(tiles[i, j].transform.localPosition);
                        lightning.RunVFX();
                    }
            }
    }
}
