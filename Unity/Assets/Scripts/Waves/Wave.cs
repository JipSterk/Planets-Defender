using System;
using UnityEngine;

namespace PlanetDefenders.Waves
{
    [Serializable]
    public struct Wave
    {
        public string Name { get { return _name; } }
        public int AmountToSpawn { get { return _amountToSpawn; } }
        public Comet[] CometPrefabs { get { return _cometPrefabs; } }

        [SerializeField] private string _name;
        [SerializeField] private int _amountToSpawn;
        [SerializeField] private Comet[] _cometPrefabs;
    }
}