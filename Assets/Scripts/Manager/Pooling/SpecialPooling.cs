using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager.Pooling
{
    internal class SpecialPooling : MonoBehaviour, ISpecialPooling
    {
        public GameObject[] potionPrefabs;
        public Transform potionContainer;
        ISpecialFactory specialFactory;

        private void Awake()
        {
            specialFactory = new SpecialFactory();
        }

        public Dictionary<int, List<SpecialController>> Create(int spawnNumber)
        {
            Dictionary<int, List<SpecialController>> potionDict = new();
            for (int i = 0; i < potionPrefabs.Length; i++)
            {
                potionDict.TryAdd(i,
                    specialFactory.Create(potionPrefabs[i], potionContainer, spawnNumber / 2));
            }

            return potionDict;
        }

        public List<SpecialController> Create(int type, int spawnNumber)
        {
            return specialFactory.Create(potionPrefabs[type], potionContainer, spawnNumber);
        }
    }
}
