using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlanetDefenders.Planets
{
    public class PlanetManager : MonoBehaviour
    {
        public static PlanetManager Instance { get { return _instance ?? new GameObject("Planet Manager").AddComponent<PlanetManager>(); } }

        private static PlanetManager _instance;
        private List<Planet> _planets = new List<Planet>();
        private GameManager _gameManager;

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
            _planets = FindObjectsOfType<Planet>().ToList();
            _gameManager = GameManager.Instance;
        }

        public void InitAllPlanets(Action<Planet> callback)
        {
            callback += planet =>
            {
                _planets.Remove(planet);

                if (_planets.Count <= 0)
                    _gameManager.EndGame();
            };

            _planets.ForEach(x => x.OnPlanetDestroyed += callback);
        }

        public Planet GetRandomPlanet()
        {
            return _planets.Random();
        }
    }
}