using System.Collections.Generic;
using UnityEngine;

public class LightningFactory : ILightningFactory
{
    public List<LightningVFX> Create(GameObject potionPrefab,
        Transform vfxContainer, int spawnNumber)
    {
        if (spawnNumber <= 0)
            return null;

        List<LightningVFX> vfxs = new List<LightningVFX>();
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject vfx = GameObject.Instantiate(potionPrefab, vfxContainer);
            vfx.gameObject.SetActive(false);
            vfxs.Add(vfx.GetComponent<LightningVFX>());
        }
        return vfxs;
    }
}
