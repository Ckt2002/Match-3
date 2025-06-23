using System.Collections.Generic;
using UnityEngine;

public class LightningPooling : MonoBehaviour, ILightningPooling
{
    public GameObject lightningPrefab;
    public Transform lightningContainer;
    ILightningFactory lightningFactory;

    private void Awake()
    {
        lightningFactory = new LightningFactory();
    }

    public List<LightningVFX> Create(int spawnNumber)
    {
        return lightningFactory.Create(lightningPrefab, lightningContainer, spawnNumber);
    }
}