using System;
using System.Collections.Generic;
using System.Linq;
using PlanetDefenders.Planets;
using UnityEngine;

namespace PlanetDefenders.Waves
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance { get { return _instance ?? new GameObject("Wave Manager").AddComponent<WaveManager>(); } }
        public event Action<Wave> NewWave;

        [SerializeField] private Wave[] _waves;
        [SerializeField] private Transform[] _spawnPoints;

        private static WaveManager _instance;
        private readonly List<Comet> _comets = new List<Comet>();
        private PlanetManager _planetManager;
        
        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _planetManager = PlanetManager.Instance;
            _planetManager.InitAllPlanets(DestroyAllComets);

            NewWave += SpawnWaveOfComets;

            SpawnWaveOfComets(_waves.First());
        }

        private void SpawnWaveOfComets(Wave wave)
        {
            for (var i = 0; i < wave.AmountToSpawn; i++)
            {
                var comet = wave.CometPrefabs.Random();
                var tempComet = Instantiate(comet, _spawnPoints.Random().position, Quaternion.identity, transform);
                tempComet.Init(_planetManager.GetRandomPlanet(), CheckForAliveComets);
                _comets.Add(tempComet);
            }
        }

        private void CheckForAliveComets(Comet comet)
        {
            _comets.Remove(comet);
            Destroy(comet.gameObject);

            if(_comets.Count > 0)
                return;

            if (NewWave != null)
                NewWave(_waves.Random());
        }
        
        //private Vector3 GetRandomSpawnPosition()
        //{
        //    var x = Random
        //}

        private void DestroyAllComets(Planet planet)
        {
            Debug.LogFormat("Planet: {0} died.", planet.Name);

            _comets.ForEach(x => Destroy(x.gameObject));
            _comets.Clear();
        }
    }
}