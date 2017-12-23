using PlanetDefenders.Planets;
using UnityEngine;

namespace PlanetDefenders.Waves
{
    public class MotherShip : MonoBehaviour
    {
        public float speed;
        private Vector3 destination;

        void Start()
        {
            PlanetManager.Instance.GetRandomPlanet();
        }

        void Update()
        {
        }
    }
}