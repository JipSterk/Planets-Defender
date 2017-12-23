using System;
using UnityEngine;

namespace PlanetDefenders.Planets
{
    public class Planet : MonoBehaviour
    {
        public string Name { get { return _name; } }
        public int Health { get { return _health; } }

        public event Action<Planet> OnPlanetAttacked;
        public event Action<Planet> OnPlanetDestroyed;
        
        [SerializeField] private string _name;
        [SerializeField] private int _health;
        [SerializeField] private ParticleSystem _planetExplosion;
        
        private bool _isdead;
        
        private void Start()
        {
            name = string.Format("Planet: {0}", _name);
        }
        
        public void TakeDamage(int damage)
        {
            if(_isdead)
                return;

            _health -= damage;

            if (_health > 0)
            {
                if (OnPlanetAttacked != null)
                    OnPlanetAttacked(this);

                return;
            }

            Die();
        }

        private void Die()
        {
            _isdead = true;

            if (OnPlanetDestroyed != null)
                OnPlanetDestroyed(this);

            var planetExplosion = Instantiate(_planetExplosion, transform.position, Quaternion.identity);
            Destroy(planetExplosion.gameObject, planetExplosion.main.duration);
            Destroy(gameObject);
        }
    }
}